using System;
using System.IO;
using System.Runtime.Remoting;
using System.Windows.Forms;

namespace Server
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            // Read configuration xml file

            RemotingConfiguration.Configure("Server.exe.config", false);             
            // Wait for exit
         /*   Console.WriteLine("Service started. Press enter to exit.");
            Console.ReadKey();*/

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);            
            Application.Run(new ServerHome());
        }
    }
}