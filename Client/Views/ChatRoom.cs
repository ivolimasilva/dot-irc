using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Xml.Linq;

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
            FileSystemWatcher watcher = new FileSystemWatcher();
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
            XDocument file = XDocument.Load(e.FullPath);

            messages =
                file.Root
                .Elements("Message")
                .Select(_message => new Common.Message(
                    (string)_message.Element("Source"),
                    (string)_message.Element("Destination"),
                    (string)_message.Element("Content"))).ToList();

            foreach (var message in messages)
            {
                if (this.rtbMessages.InvokeRequired)
                {
                    this.rtbMessages.BeginInvoke((MethodInvoker)delegate () { rtbMessages.AppendText(message.Content()); });
                }
                else
                {
                    rtbMessages.AppendText(message.Content());
                }
            }
        }

        private void btnSendMsg_Click(object sender, EventArgs e)
        {
            if (txtboxChat.Text.Trim().Length > 0)
            {
                Common.Message msgInfo = new Common.Message(userSource.username, userDestination.username, txtboxChat.Text);
                remoteClient.send(msgInfo);

                //update(msgInfo);
                txtboxChat.Text = "";
            }
        }

        private void update(Common.Message msgInfo)
        {
            /*
            rchtxtboxChat.AppendText(DateTime.Now.ToString("h:mm tt") + "- " + msgInfo.Source() + " : " + msgInfo.Content() + "\n");
            if (userSource.username == msgInfo.Source())
                rchtxtboxChat.SelectionColor = System.Drawing.Color.Green;
            */
            //rchtxtboxChat.AppendText(msgInfo.Content());
        }
    }
}
