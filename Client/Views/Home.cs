using Client.Remotes;
using Client.Views;
using System.Windows.Forms;

namespace Client
{
    public partial class Home : Form
    {
        private Auth auth;
        //static ChatRoom chatRoom;

        public Home()
        {
            InitializeComponent();
            auth = new Auth();
            
        }

        private void btnRegister_Click(object sender, System.EventArgs e)
        {
            if (auth.register(txtUsername.Text, txtName.Text, txtPassword.Text))
            {
                lblStatus.Text = "User registered. You may login.";
                txtUsername.Text = "";
                txtName.Text = "";
                txtPassword.Text = "";
            }                
            else
                lblStatus.Text = "Registration failed.";
        }

        private void btnLogin_Click(object sender, System.EventArgs e)
        {
            if(auth.login(txtUsernameLogin.Text, txtPasswordLogin.Text))
            {
                //create something to destroy this form
                lblStatusLogin.Text = "Login succeded, creating ChatRoom";
                //this.Close();
                //this.Hide();
                this.Visible = false;
                ChatRoom chatRoom = new ChatRoom();
                chatRoom.getUserName(txtUsernameLogin.Text);
                chatRoom.ShowDialog();
                txtUsernameLogin.Text = "";
                txtPasswordLogin.Text = "";
                this.Close();
                //this.Visible = true;
            }
            else lblStatusLogin.Text = "Login failed, try again.";
        }
    }
}