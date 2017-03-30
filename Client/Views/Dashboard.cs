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

        // List of users without logged user
        private List<User> filteredUsers;

        private string filename;
        private FileSystemWatcher watcher = new FileSystemWatcher();

        public Dashboard(User _user)
        {
            InitializeComponent();
            remoteAuth = (IAuth)RemoteNew.New(typeof(IAuth));
            UserHandlerEventRepeater userRepeater = new UserHandlerEventRepeater();

            // Set current user
            user = _user;
            lblUserName.Text = "Logged as " + user.name;

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

            updateUserList(users);

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

        // Define the event handlers.
        private void OnChanged(object source, FileSystemEventArgs e)
        {
            List<Common.Message> messages = new List<Common.Message>();

            XDocument file;
            using (var mutex = new Mutex(false, "Message" + user.username))
            {
                mutex.WaitOne();
                file = XDocument.Load(e.FullPath);

                // Load the list with all messages
                messages =
                    file.Root
                    .Elements("Message")
                    .Select(_message => new Common.Message(
                        (string)_message.Element("Source"),
                        (string)_message.Element("Destination"),
                        (string)_message.Element("Content"),
                        (bool)_message.Element("End"))).ToList();

                // Remove messages from another conversations
                messages.RemoveAll(_message => _message.End());

                XElement fileSave = new XElement("Messages",
                            from message in messages
                            select new XElement("Message",
                            new XAttribute("ID", messages.IndexOf(message)),
                            new XElement("Source", message.Source()),
                            new XElement("Destination", message.Destination()),
                            new XElement("Content", message.Content()),
                            new XElement("End", message.End())));

                fileSave.Save(filename);
                mutex.ReleaseMutex();
            }
        }

        private void userListener(List<User> _users)
        {
            users = _users;
            filteredUsers = users.Where(_user => _user.username != user.username).ToList();
        }

        private void closeDashBoard(object sender, FormClosedEventArgs e)
        {
            watcher.Dispose();
            remoteAuth.logout(user.username);
            Application.Exit();
        }

        private void updateUserList(List<User> users)
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
                    // TODO
                }
            }
        }
    }
}