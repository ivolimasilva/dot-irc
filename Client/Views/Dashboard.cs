using Client.Remotes;
using Client.Utils;
using Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Client.Views
{
    public partial class Dashboard : Form
    {
        // Represents the logged user
        private User user;

        // Represents the list of users
        private List<User> users;

        // Remote object for the Authentication module
        private IAuth remoteAuth;

        // Remote object for the Messages module
        private IMessages remoteMessages;

        private List<Common.Message> globalMessages = new List<Common.Message>();

        // List of users without logged user
        private List<User> filteredUsers;

        // List of users with ongoing conversation
        private List<string> conversations = new List<string>();

        private string filename;
        private FileSystemWatcher watcher = new FileSystemWatcher();

        public Dashboard(User _user)
        {
            InitializeComponent();
            remoteAuth = (IAuth)RemoteNew.New(typeof(IAuth));
            UserHandlerEventRepeater userRepeater = new UserHandlerEventRepeater();

            remoteMessages = (IMessages)RemoteNew.New(typeof(IMessages));
            MessageHandlerEventRepeater messageRepeater = new MessageHandlerEventRepeater();

            messageRepeater.onChange += new MessageHandler(messageListener);
            remoteMessages.onChange += new MessageHandler(messageRepeater.Repeater);

            // Set current user
            user = _user;
            lblUserName.Text = "Logged as " + user.name;

            globalMessages = remoteMessages.getMessages();
            UpdateMessages();

            // Delete & Create messages file
            filename = "messages-" + user.username + ".xml";
            using (var mutex = new Mutex(false, "Message" + user.username))
            {
                XElement file = new XElement("Messages");
                file.Save(filename);
            }

            // Register own channel
            TcpChannel channel = (TcpChannel)Remoting.GetChannel(user.port, false);
            ChannelServices.RegisterChannel(channel, false);

            // Get list of users and set listener for users' changes
            users = remoteAuth.Users();
            filteredUsers = users.Where(_tmp => _tmp.username != user.username).ToList();

            updateUserList();

            userRepeater.onChange += new UserHandler(userListener);
            remoteAuth.onChange += new UserHandler(userRepeater.Repeater);

            #region File watcher
            watcher.Path = ".";
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite;
            watcher.Filter = filename;

            // Add event handlers.
            watcher.Changed += new FileSystemEventHandler(OnChanged);

            // Begin watching.
            watcher.EnableRaisingEvents = true;
            #endregion
        }

        private void messageListener(List<Common.Message> _messages)
        {
            globalMessages = _messages;
            UpdateMessages();
        }

        private void UpdateMessages()
        {
            if (rtbChat.InvokeRequired)
                rtbChat.BeginInvoke((MethodInvoker)delegate ()
                {
                    rtbChat.Clear();
                });
            else
                rtbChat.Clear();

            // In case of the other messages
            foreach (Common.Message message in globalMessages)
            {
                if (rtbChat.InvokeRequired)
                {
                    rtbChat.BeginInvoke((MethodInvoker)delegate ()
                    {
                        if (user.username == message.Source())
                            rtbChat.SelectionColor = Color.Green;
                        else
                            rtbChat.SelectionColor = Color.Black;

                        rtbChat.AppendText(message.Source() + ": " + message.Content());
                        rtbChat.AppendText(Environment.NewLine);
                    });
                }
                else
                {
                    if (user.username == message.Source())
                        rtbChat.SelectionColor = Color.Green;
                    else
                        rtbChat.SelectionColor = Color.Black;

                    rtbChat.AppendText(message.Source() + ": " + message.Content());
                    rtbChat.AppendText(Environment.NewLine);
                }
            }
        }

        // Define the event handlers.
        private void OnChanged(object source, FileSystemEventArgs e)
        {
            List<Common.Message> messages = new List<Common.Message>();

            XDocument file;
            using (var mutex = new Mutex(false, "Message" + user.username))
            {
                mutex.WaitOne();
                file = XDocument.Load(e.FullPath);
                mutex.ReleaseMutex();
            }

            // Load the list with all messages
            messages =
                file.Root
                .Elements("Message")
                .Select(_message => new Common.Message(
                    (string)_message.Element("Source"),
                    (string)_message.Element("Destination"),
                    (string)_message.Element("Content"),
                    (bool)_message.Element("End"))).ToList();

            // Search for ongoing conversations
            conversations.Clear();

            for (int i = 0; i < messages.Count; i++)
            {
                if (messages[i].End() && messages[i].Content() == "---")
                {
                    String _user = messages[i].Source() == user.username ? messages[i].Destination() : messages[i].Source();
                    conversations.Add(_user);
                    for (int j = i + 1; j < messages.Count; j++)
                    {
                        if (messages[j].End() && messages[j].Content() == "" && (messages[j].Source() == _user || messages[j].Destination() == _user))
                        {
                            conversations.Remove(_user);
                        }
                    }
                }
            }

            UpdateBtnStart();
        }

        private void userListener(List<User> _users)
        {
            users = _users;
            filteredUsers = users.Where(_user => _user.username != user.username).ToList();

            listUsers.DataSource = filteredUsers;

            UpdateBtnStart();
        }

        private void UpdateBtnStart()
        {
            if (btnStartChat.InvokeRequired)
                btnStartChat.BeginInvoke((MethodInvoker)delegate ()
                {
                    btnStartChat.Enabled = !conversations.Contains(filteredUsers[listUsers.SelectedIndex].username) && filteredUsers[listUsers.SelectedIndex].online;
                });
            else
                btnStartChat.Enabled = !conversations.Contains(filteredUsers[listUsers.SelectedIndex].username) && filteredUsers[listUsers.SelectedIndex].online;
        }

        private void closeDashBoard(object sender, FormClosedEventArgs e)
        {
            watcher.Dispose();
            remoteAuth.logout(user.username);

            // This is a forced exit - but oh well, who cares?
            Environment.Exit(Environment.ExitCode);
        }

        private void updateUserList()
        {
            listUsers.DataSource = filteredUsers;
            listUsers.DrawMode = DrawMode.OwnerDrawFixed;
            listUsers.DrawItem += new DrawItemEventHandler(listUsers_DrawItem);
            listUsers.DisplayMember = "Name";
        }

        private void listUsers_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Draw the background of the ListBox control for each item.
            e.DrawBackground();
            // Define the default color of the brush as black.
            Brush myBrush = Brushes.Black;
            FontStyle myFont = FontStyle.Regular;

            if (filteredUsers.Count > 0)
            {
                switch (filteredUsers[e.Index].online)
                {
                    case true:
                        myBrush = Brushes.Green;
                        myFont = FontStyle.Bold;
                        break;
                    case false:
                        myBrush = Brushes.Gray;
                        break;
                }
                // Draw the current item text based on Font  and the custom brush settings.
                e.Graphics.DrawString(filteredUsers[e.Index].username, new Font("Calibri", 11.75F, myFont), myBrush, e.Bounds, StringFormat.GenericDefault);
            }

            // If the ListBox has focus, draw a focus rectangle around the selected item.
            e.DrawFocusRectangle();

        }

        private void btnStartChat_Click(object sender, EventArgs e)
        {
            if (listUsers.SelectedIndex != -1)
            {
                User userSelected = filteredUsers[listUsers.SelectedIndex];
                String url = "tcp://" + userSelected.ip + ":" + userSelected.port + "/Request";

                var channels = ChannelServices.RegisteredChannels;

                // Check if Client was already registered
                if (RemotingConfiguration.GetRegisteredWellKnownClientTypes().Any(client => client.ObjectUrl == url))
                    RemotingConfiguration.RegisterWellKnownClientType(new WellKnownClientTypeEntry(typeof(IRequests), url));

                IRequests remoteRequests = (IRequests)Activator.GetObject(typeof(IRequests), url);

                if (remoteRequests.ask(user, userSelected))
                {
                    // Open chatroom
                    new Thread(() =>
                    {
                        Application.Run(new ChatRoom(user, userSelected));
                    }).Start();
                }
                else
                {
                    // Other user declined
                    MessageBox.Show(userSelected.name + " declined your conversation.", "Conversation denied.", MessageBoxButtons.OK);
                }
            }
        }

        private void listUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateBtnStart();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (txtChat.Text.Trim().Length > 0)
            {
                Common.Message _message = new Common.Message(user.username, "ALL", txtChat.Text);
                remoteMessages.send(_message);

                // Clear input box
                txtChat.Clear();
            }
        }
    }
}