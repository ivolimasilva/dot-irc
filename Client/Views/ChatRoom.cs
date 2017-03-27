using Common;
using System;
using System.Windows.Forms;

namespace Client.Views
{
    public partial class ChatRoom : Form
    {
        private IAuth remoteAuth;
        private User user;

        public ChatRoom(User user)
        {
            //TODO alterar o Text para o nome do receptor das msgs
            this.Text = user.username;
            InitializeComponent();
        }

        private void btnSendMsg_Click(object sender, EventArgs e)
        {
            if(txtboxChat.Text.Trim().Length > 0)
            {
                // TODO send message here
                txtboxChat.Text = "";
            }
        }

       /* private void closeChatRoom(object sender, FormClosedEventArgs e)
        {
            remoteAuth.logout(user.username);
            this.Close();
        }*/        
    }
}
