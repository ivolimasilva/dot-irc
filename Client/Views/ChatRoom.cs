using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.Views
{
    public partial class ChatRoom : Form
    {
        private static List<User> users = new List<User>();

        public ChatRoom()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            SendMessage();
        }

        private void SendMessage()
        {
            if(txtboxChat.Text.Trim().Length >0)
            {
                // Creates the call to server, sending the message with txtboxChat.Text
                txtboxChat.Text = "";
            }
        }

        private void closeChatRoom(object sender, EventArgs e)
        {
            /*if (remoteObj != null)
            {
                remoteObj.LeaveChatRoom(yourName);
                txtChatHere.Text = "";
            }*/

            // Create a call to the server that places the user offline            
            Application.Exit();
        }
    }
}
