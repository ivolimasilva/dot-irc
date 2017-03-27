using Client.Remotes;
using Client.Utils;
using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Windows.Forms;

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

        private List<User> filteredUsers;

        public Dashboard(User _user)
        {
            InitializeComponent();
            remoteAuth = (IAuth)RemoteNew.New(typeof(IAuth));
            UserHandlerEventRepeater userRepeater = new UserHandlerEventRepeater();

            // Set current user
            user = _user;
            lblUserName.Text = "Logged as " + user.name;

            // Register own channel
            TcpChannel channel = (TcpChannel)Remoting.GetChannel(user.port, false);
            ChannelServices.RegisterChannel(channel, false);

            // Get list of users and set listener for users' changes
            users = remoteAuth.Users();
            filteredUsers = users.Where(_tmp => _tmp.username != user.username).ToList();

            updateUserList(users);

            userRepeater.onChange += new UserHandler(userListener);
            remoteAuth.onChange += new UserHandler(userRepeater.Repeater);
        }

        private void userListener(List<User> _users)
        {
            users = _users;
            filteredUsers = users.Where(_user => _user.username != user.username).ToList();
        }

        private void closeDashBoard(object sender, FormClosedEventArgs e)
        {
            remoteAuth.logout(user.username);
            this.Close();
        }

        private void updateUserList(List<User> users)
        {
            // Removes current logged user from the list
            // Not working as intended :/ 
            //users.Remove(user);

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
                String url = "tcp://" + ((User)listUsers.SelectedValue).ip + ":" + ((User)listUsers.SelectedValue).port + "/Request";

                var channels = ChannelServices.RegisteredChannels;

                // Check if Client was already registered
                if (RemotingConfiguration.GetRegisteredWellKnownClientTypes().Any(client => client.ObjectUrl == url))
                    RemotingConfiguration.RegisterWellKnownClientType(new WellKnownClientTypeEntry(typeof(IRequests), url));

                IRequests remoteRequests = (IRequests)Activator.GetObject(typeof(IRequests), url);

                if (remoteRequests.ask(user))
                {
                    // Open chatroom
                    ChatRoom chatRoom = new ChatRoom(user);
                    chatRoom.Show();
                }
                else
                {
                    // Other user declined
                }

                /*
                // Next 2 lines are for "debugging"/information         
                string text = "Talk to " + ((User)listUsers.SelectedValue).username + "?";
                MessageBox.Show(text);

                ChatRoom chatRoom = new ChatRoom(user);
                //chatRoom.ShowDialog();
                chatRoom.Show();
                //this.Close();
                */
            }
        }
    }
}