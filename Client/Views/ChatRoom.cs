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
    public partial class ChatRoom : Form
    {
        private IMessages remoteClient;
        private User userSource, userDestination;
        private Messages msg;        
        public static ChatRoom _ChatRoom;

        public ChatRoom(User _userSource,User _userDestination)
        {
            InitializeComponent();
            userSource = _userSource;
            userDestination = _userDestination;
            this.Text = userDestination.username;

            TcpChannel channel = (TcpChannel)Remoting.GetChannel(userSource.port, false);
            ChannelServices.RegisterChannel(channel, false);            
            string url = "tcp://" + userDestination.ip + ":" + userDestination.port + "/Message";

            if (RemotingConfiguration.GetRegisteredWellKnownClientTypes().Any(client => client.ObjectUrl == url))
                RemotingConfiguration.RegisterWellKnownClientType(new WellKnownClientTypeEntry(typeof(IMessages), url));

            remoteClient = (IMessages)Activator.GetObject(typeof(IMessages), url);
        }

        private void btnSendMsg_Click(object sender, EventArgs e)
        {
            if(txtboxChat.Text.Trim().Length > 0)
            {
                Common.Message msgInfo = new Common.Message(userSource.username,userDestination.username,txtboxChat.Text);
                remoteClient.send(msgInfo);

                update(msgInfo);
                txtboxChat.Text = "";
            }
        }

        private void update(Common.Message msgInfo)
        {
            rchtxtboxChat.AppendText(DateTime.Now.ToString("h:mm tt")+"- " + msgInfo.Source() + " : " + msgInfo.Content()+"\n");
            if(userSource.username == msgInfo.Source())
                rchtxtboxChat.SelectionColor = System.Drawing.Color.Green;
        }

       /* private void closeChatRoom(object sender, FormClosedEventArgs e)
        {
            remoteAuth.logout(user.username);
            this.Close();
        }*/        
    }
}
