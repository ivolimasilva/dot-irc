using System;
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
        }
    }
}
