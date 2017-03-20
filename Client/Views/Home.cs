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
    }
}