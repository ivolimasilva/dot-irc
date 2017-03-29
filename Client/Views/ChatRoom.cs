using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting;
using System.Security.Permissions;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;
using Client.Utils;

namespace Client.Views
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
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

            #region File watcher
            watcher.Path = ".";
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite;
            watcher.Filter = "messages-" + userSource.username + ".xml";

            // Add event handlers.
            watcher.Created += new FileSystemEventHandler(OnChanged);
            watcher.Changed += new FileSystemEventHandler(OnChanged);

            // Begin watching.
            watcher.EnableRaisingEvents = true;
            #endregion
        }

        // Define the event handlers.
        private void OnChanged(object source, FileSystemEventArgs e)
        {
            XDocument file;
            using (var mutex = new Mutex(false, "Message"))
            {
                mutex.WaitOne();
                file = XDocument.Load(e.FullPath);
                mutex.ReleaseMutex();
            }

            messages.Clear();

            messages =
                file.Root
                .Elements("Message")
                .Select(_message => new Common.Message(
                    (string)_message.Element("Source"),
                    (string)_message.Element("Destination"),
                    (string)_message.Element("Content"))).ToList();

            update();
        }

        private void btnSendMsg_Click(object sender, EventArgs e)
        {
            if (txtboxChat.Text.Trim().Length > 0)
            {
                Common.Message _message = new Common.Message(userSource.username, userDestination.username, txtboxChat.Text);
                remoteClient.send(_message);

                messages.Add(_message);
                update();

                // Save Messages to a file
                try
                {
                    string filename = "./messages-" + _message.Destination() + ".xml";

                    XElement file = new XElement("Messages",
                        from message in messages
                        select new XElement("Message",
                        new XAttribute("ID", messages.IndexOf(message)),
                        new XElement("Source", message.Source()),
                        new XElement("Destination", message.Destination()),
                        new XElement("Content", message.Content())));

                    using (var mutex = new Mutex(false, "Message"))
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

                txtboxChat.Clear();
            }
        }

        private void update()
        {
            if (rtbMessages.InvokeRequired)
                rtbMessages.BeginInvoke((MethodInvoker)delegate ()
                {
                    rtbMessages.Clear();
                });
            else
                rtbMessages.Clear();

            foreach (var message in messages)
            {
                if (rtbMessages.InvokeRequired)
                {
                    rtbMessages.BeginInvoke((MethodInvoker)delegate ()
                    {
                        if (userSource.username == message.Source())
                            rtbMessages.SelectionColor = System.Drawing.Color.Green;
                        else
                            rtbMessages.SelectionColor = System.Drawing.Color.Black;

                        rtbMessages.AppendText(message.Source() + " : " + message.Content());
                        rtbMessages.AppendText(Environment.NewLine);
                    });
                }
                else
                {
                    if (userSource.username == message.Source())
                        rtbMessages.SelectionColor = System.Drawing.Color.Green;
                    else
                        rtbMessages.SelectionColor = System.Drawing.Color.Black;

                    rtbMessages.AppendText(message.Source() + " : " + message.Content());
                    rtbMessages.AppendText(Environment.NewLine);
                }
            }
        }

        private void closeChatroom(object sender, FormClosedEventArgs e)
        {
            // Clear messages file
            File.Delete("./messages-" + userSource.username + ".xml");

            // Clear watcher
            watcher.Dispose();
        }
    }
}