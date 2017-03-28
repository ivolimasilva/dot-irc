using Client.Remotes;
using Client.Utils;
using Common;
using System;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Windows.Forms;

namespace Client.Views
{
    public partial class ChatRoom : Form, IPrivateMessages
    {
        // Remote object for the other client
        private IPrivateMessages remoteClient;

        // Represents the logged user
        private User userSource;

        // Represents the destination user
        private User userDestination;

        public ChatRoom(User _userSource, User _userDestination)
        {
            InitializeComponent();

            userSource = _userSource;
            userDestination = _userDestination;

            this.Text = userDestination.name;

            RemotingConfiguration.RegisterWellKnownServiceType(new WellKnownServiceTypeEntry(typeof(IPrivateMessages), "/Message", WellKnownObjectMode.Singleton));

            #region WellKnwown Client Registration
            string url = "tcp://" + userDestination.ip + ":" + userDestination.port + "/Message";

            if (RemotingConfiguration.GetRegisteredWellKnownClientTypes().Any(client => client.ObjectUrl == url))
                RemotingConfiguration.RegisterWellKnownClientType(new WellKnownClientTypeEntry(typeof(IPrivateMessages), url));

            remoteClient = (IPrivateMessages)Activator.GetObject(typeof(IPrivateMessages), url);
            #endregion
        }

        private void btnSendMsg_Click(object sender, EventArgs e)
        {
            if (txtboxChat.Text.Trim().Length > 0)
            {
                Common.Message msgInfo = new Common.Message(userSource.username, userDestination.username, txtboxChat.Text);
                remoteClient.send(msgInfo);

                update(msgInfo);
                txtboxChat.Text = "";
            }
        }

        private void update(Common.Message msgInfo)
        {
            rchtxtboxChat.AppendText(DateTime.Now.ToString("h:mm tt") + "- " + msgInfo.Source() + " : " + msgInfo.Content() + "\n");
            if (userSource.username == msgInfo.Source())
                rchtxtboxChat.SelectionColor = System.Drawing.Color.Green;
        }

        public void send(Common.Message _message)
        {
            // Receives message here - don't mistake with remoteClient.send(...)
            var msg = _message;
            update(_message);
        }
    }
}
