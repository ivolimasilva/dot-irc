using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Client.Views
{
    public partial class ChatRoom : Form
    {
        // Remote object for the other client
        private IPrivateMessages remoteClient;

        // Represents the logged user
        private User userSource;

        // Represents the destination user
        private User userDestination;

        private FileSystemWatcher watcher = new FileSystemWatcher();

        private List<Common.Message> messages = new List<Common.Message>();

        private string filename;

        public ChatRoom(User _userSource, User _userDestination)
        {
            InitializeComponent();

            userSource = _userSource;
            userDestination = _userDestination;

            this.Text = userDestination.name;

            #region WellKnwown Client Registration
            string url = "tcp://" + userDestination.ip + ":" + userDestination.port + "/Message";

            if (RemotingConfiguration.GetRegisteredWellKnownClientTypes().Any(client => client.ObjectUrl == url))
                RemotingConfiguration.RegisterWellKnownClientType(new WellKnownClientTypeEntry(typeof(IPrivateMessages), url));

            remoteClient = (IPrivateMessages)Activator.GetObject(typeof(IPrivateMessages), url);
            #endregion

            #region Load messages from file
            filename = "messages-" + userSource.username + ".xml";

            LoadMessages(true);

            // Add empty message in the file
            messages.Add(new Common.Message(userSource.username, userDestination.username, "---", true));

            XElement fileSave = new XElement("Messages",
                            from message in messages
                            select new XElement("Message",
                            new XAttribute("ID", messages.IndexOf(message)),
                            new XElement("Source", message.Source()),
                            new XElement("Destination", message.Destination()),
                            new XElement("Content", message.Content()),
                            new XElement("End", message.End())));

            using (var mutex = new Mutex(false, "Message" + userSource.username))
            {
                mutex.WaitOne();
                fileSave.Save(filename);
                mutex.ReleaseMutex();
            }

            UpdateMessages();
            #endregion

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
            LoadMessages(false);
        }

        private void LoadMessages(bool _firstTime)
        {
            messages.Clear();

            XDocument file;
            using (var mutex = new Mutex(false, "Message" + userSource.username))
            {
                mutex.WaitOne();
                file = XDocument.Load(filename);
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

            // Remove messages from another conversations
            messages.RemoveAll(_message => !(_message.Source() == userDestination.username || _message.Destination() == userDestination.username));

            if (!_firstTime)
                UpdateMessages();
        }

        private void btnSendMsg_Click(object sender, EventArgs e)
        {
            if (txtboxChat.Text.Trim().Length > 0)
            {
                Common.Message _message = new Common.Message(userSource.username, userDestination.username, txtboxChat.Text);
                remoteClient.send(_message);

                // Save Messages to a file
                try
                {
                    messages.Add(_message);

                    XElement file = new XElement("Messages",
                        from message in messages
                        select new XElement("Message",
                        new XAttribute("ID", messages.IndexOf(message)),
                        new XElement("Source", message.Source()),
                        new XElement("Destination", message.Destination()),
                        new XElement("Content", message.Content()),
                        new XElement("End", message.End())));

                    using (var mutex = new Mutex(false, "Message" + userSource.username))
                    {
                        mutex.WaitOne();
                        file.Save(filename);
                        mutex.ReleaseMutex();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                // Clear input box
                txtboxChat.Clear();
            }
        }

        private void UpdateMessages()
        {
            if (rtbMessages.InvokeRequired)
                rtbMessages.BeginInvoke((MethodInvoker)delegate ()
                {
                    rtbMessages.Clear();
                });
            else
                rtbMessages.Clear();

            // If the last message it an end
            if (messages.Count > 0)
                if (messages.Last().End() && messages.Last().Content() == "")
                {
                    if (MessageBox.Show(userDestination.name + " has closed the conversation.", "End of conversation", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        watcher.Dispose();

                        if (this.InvokeRequired)
                        {
                            this.BeginInvoke((MethodInvoker)delegate ()
                            {
                                this.Close();
                            });
                        }
                        else
                        {
                            this.Close();
                        }
                    }
                }

            // In case of the other messages
            foreach (var message in messages)
            {
                if (!message.End())
                {
                    // Normal message

                    if (rtbMessages.InvokeRequired)
                    {
                        rtbMessages.BeginInvoke((MethodInvoker)delegate ()
                        {
                            if (userSource.username == message.Source())
                                rtbMessages.SelectionColor = System.Drawing.Color.Green;
                            else
                                rtbMessages.SelectionColor = System.Drawing.Color.Black;

                            rtbMessages.AppendText(message.Source() + ": " + message.Content());
                            rtbMessages.AppendText(Environment.NewLine);
                        });
                    }
                    else
                    {
                        if (userSource.username == message.Source())
                            rtbMessages.SelectionColor = System.Drawing.Color.Green;
                        else
                            rtbMessages.SelectionColor = System.Drawing.Color.Black;

                        rtbMessages.AppendText(message.Source() + ": " + message.Content());
                        rtbMessages.AppendText(Environment.NewLine);
                    }
                }
            }
        }

        private void closeChatroom(object sender, FormClosedEventArgs e)
        {
            // Send end message
            remoteClient.send(new Common.Message(userSource.username, userDestination.username, true));

            watcher.Dispose();
        }
    }
}