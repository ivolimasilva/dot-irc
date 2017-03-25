using System;
using System.IO;
using System.Runtime.Remoting;

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
            Console.WriteLine("Service started. Press enter to exit.");
            Console.ReadKey();
        }
    }
}