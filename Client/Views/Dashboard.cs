using Client.Remotes;
using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Client.Views
{
    public partial class Dashboard : Form
    {
        private List<User> users;
        private IAuth remoteAuth;
        private User user;

        public Dashboard(User _user)
        {
            InitializeComponent();
            remoteAuth = (IAuth)RemoteNew.New(typeof(IAuth));
            UserHandlerEventRepeater userRepeater = new UserHandlerEventRepeater();

            // Set current user
            user = _user;
            lblUserName.Text = "Logged as " + user.name;

            // Initialize list of users
            // users = new List<User>();

            // Get list of users and set listener for users' changes
            users = remoteAuth.Users();
            updateUserList(users);

            userRepeater.onChange += new UserHandler(userListener);
            remoteAuth.onChange += new UserHandler(userRepeater.Repeater);
        }

        private void userListener(List<User> _users)         
        {
            users = _users;
        }

        private void closeChatRoom(object sender, FormClosedEventArgs e)
        {
            remoteAuth.logout(user.username);
            this.Close();
        }

        private void updateUserList(List<User> users)
        {                        
            // Removes current logged user from the list
            // Not working as intended :/ 
            users.Remove(user);

            listUsers.DataSource = users;
          //  listUsers.Items.Remove(user); // this breaks the program
            listUsers.DrawMode = DrawMode.OwnerDrawFixed;       
            listUsers.DrawItem += new DrawItemEventHandler(listUsers_DrawItem);
            listUsers.DisplayMember = "Username";            
        }

        private void listUsers_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Draw the background of the ListBox control for each item.
            e.DrawBackground();
            // Define the default color of the brush as black.
            Brush myBrush = Brushes.Black;
            FontStyle myFont = FontStyle.Regular;

            switch (users[e.Index].online)
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
            e.Graphics.DrawString(users[e.Index].username, new Font("Calibri", 11.75F, myFont), myBrush, e.Bounds, StringFormat.GenericDefault);
            // If the ListBox has focus, draw a focus rectangle around the selected item.
            e.DrawFocusRectangle();
        }

        private void btnStartChat_Click(object sender, EventArgs e)
        {
            if (listUsers.SelectedIndex != -1)
            {
                // TODO: Create New SingleChatClient here                
                string text = "Talk to " + ((User)listUsers.SelectedValue).username + "?";
                MessageBox.Show(text);
            }           
        }
    }
}