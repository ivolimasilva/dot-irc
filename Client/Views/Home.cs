using Common;
using Client.Remotes;
using Client.Views;
using System.Windows.Forms;
using Common.Utils;

namespace Client
{
    public partial class Home : Form
    {
        private IAuth remoteAuth;

        public Home()
        {
            InitializeComponent();
            remoteAuth = (IAuth)RemoteNew.New(typeof(IAuth));
        }

        private void btnRegister_Click(object sender, System.EventArgs e)
        {
            if (remoteAuth.register(txtUsername.Text, txtName.Text, txtPassword.Text))
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
            User user = remoteAuth.login(txtUsernameLogin.Text, txtPasswordLogin.Text, new IP(IP.GetLocalIPAddress()));

            if (user == null)
            {
                lblStatusLogin.Text = "Login failed, try again.";
            }
            else
            {
                Dashboard dashBoard = new Dashboard(user);
                this.Hide();
                dashBoard.ShowDialog();
                this.Close();
            }
        }
    }
}