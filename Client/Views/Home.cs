using Client.Remotes;
using System.Windows.Forms;

namespace Client
{
    public partial class Home : Form
    {
        private Auth auth;

        public Home()
        {
            InitializeComponent();
            auth = new Auth();
        }

        private void btnRegister_Click(object sender, System.EventArgs e)
        {
            if (auth.register(txtUsername.Text, txtName.Text, txtPassword.Text))
                lblStatus.Text = "User registered. You may login.";
            else
                lblStatus.Text = "Registration failed.";
        }

        private void btnLogin_Click(object sender, System.EventArgs e)
        {
            if(auth.login(txtUsernameLogin.Text, txtPasswordLogin.Text))
            {
                // Enter Chatroom
                lblStatusLogin.Text = "Login succeded, creating ChatRoom";
            }
            else lblStatusLogin.Text = "Login failed, try again.";
        }
    }
}