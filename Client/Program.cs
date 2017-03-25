using System;
using System.Windows.Forms;
using System.Runtime.Remoting;

namespace Client
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            // Read configuration xml file
            RemotingConfiguration.Configure("Client.exe.config", false);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Home());
        }
    }
}