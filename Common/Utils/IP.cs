using System;
using System.Net;
using System.Net.Sockets;

namespace Common.Utils
{
    [Serializable]
    public class IP
    {
        string address { get; set; }

        public IP(string _address)
        {
            address = _address;
        }

        public override string ToString()
        {
            return address;
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }
    }
}