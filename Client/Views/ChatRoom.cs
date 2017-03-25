using Client.Remotes;
using Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Client.Views
{
    public partial class ChatRoom : Form
    {
        private static List<User> users = new List<User>();
        private Auth auth;
        internal string userNickname;

        private IMessages remoteMessages;

        public ChatRoom()
        {
            InitializeComponent();
            auth = new Auth();
            updateUserList();
            listUsers.SelectedValueChanged += new EventHandler(listUsers_SelectedValueChanged);

            // Messages
            remoteMessages = (IMessages)RemoteNew.New(typeof(IMessages));
            HandlerEventRepeater evRepeater = new HandlerEventRepeater();
            evRepeater.onChange += new Handler(messageHandler);
            remoteMessages.onChange += new Handler(evRepeater.Repeater);
        }

        private void messageHandler(List<Common.Message> messages)
        {
            // Receives List of messages updated
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            SendMessage();
        }

        private void SendMessage()
        {
            if (txtboxChat.Text.Trim().Length > 0)
            {
                // Creates the call to server, sending the message with txtboxChat.Text
                txtboxChat.Text = "";
            }
        }

        public void getUserName(string userName)
        {
            userNickname = userName;
        }

        private void closeChatRoom(object sender, FormClosedEventArgs e)
        {
            auth.logout(userNickname);
            txtboxChat.Text = "";
            this.Close();
        }

        private void updateUserList()
        {
            User user1 = new User("Daniel", "Nunes", "");
            user1.online = true;
            User user2 = new User("Ivo", "Lima", "");
            user2.online = true;
            User user3 = new User("Sara", "Paiva", "");
            user3.online = true;
            User user4 = new User("Teresa", "Matos", "");
            user4.online = false;
            users.Add(user1);
            users.Add(user2);
            users.Add(user3);
            users.Add(user4);

            listUsers.DataSource = users;
            listUsers.DrawMode = DrawMode.OwnerDrawFixed;
            listUsers.DrawItem += new DrawItemEventHandler(listUsers_DrawItem);
            listUsers.DisplayMember = "Username";

            //listUsers.ValueMember = "Name";           
        }

        private void listUsers_SelectedValueChanged(object sender, EventArgs e)
        {
            if (listUsers.SelectedIndex != -1)
            {
                string text = "Talk to " + ((User)listUsers.SelectedValue).username + "?";
                MessageBox.Show(text);
            }
        }

        private void listUsers_DrawItem(object sender, DrawItemEventArgs e)
        {
            /*e.DrawBackground();
            e.Graphics.DrawString(listUsers.Items[e.Index].ToString(), new Font("Arial", 10, FontStyle.Bold), Brushes.Black, e.Bounds);
            e.DrawFocusRectangle();*/

            // Draw the background of the ListBox control for each item.
            e.DrawBackground();
            // Define the default color of the brush as black.
            Brush myBrush = Brushes.Black;

            // Determine the color of the brush to draw each item based 
            // on the index of the item to draw.            

            switch (users[e.Index].online)
            {
                case true:
                    myBrush = Brushes.Green;
                    break;
                case false:
                    myBrush = Brushes.Gray;
                    break;
            }

            // Draw the current item text based on Font  and the custom brush settings.
            //e.Graphics.DrawString(listUsers.Items[e.Index].ToString(), new Font("Calibri", 9, FontStyle.Bold), myBrush, e.Bounds, StringFormat.GenericDefault);

            e.Graphics.DrawString(users[e.Index].username, new Font("Calibri", 11.25F, FontStyle.Bold), myBrush, e.Bounds, StringFormat.GenericDefault);
            // If the ListBox has focus, draw a focus rectangle around the selected item.
            e.DrawFocusRectangle();
        }
    }
}
