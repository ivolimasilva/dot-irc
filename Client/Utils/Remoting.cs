using System;
using System.Collections;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Serialization.Formatters;

namespace Client.Utils
{
    public static class Remoting
    {
        public static string GetApplicationURL(Type Type)
        {
            ActivatedClientTypeEntry entry = null;

            foreach (ActivatedClientTypeEntry temp in RemotingConfiguration.GetRegisteredActivatedClientTypes())
            {
                if (temp.TypeName.Equals(Type.FullName))
                {
                    entry = temp;
                    break;
                }
            }

            if (entry == null)
                throw new ArgumentException(String.Format("Type {0} not found.", Type.FullName));

            return entry.ApplicationUrl;
        }

        public static IChannel GetChannel(int tcpPort, bool isSecure)
        {
            BinaryServerFormatterSinkProvider serverProv = new BinaryServerFormatterSinkProvider();
            serverProv.TypeFilterLevel = TypeFilterLevel.Full;
            IDictionary propBag = new Hashtable();
            propBag["port"] = tcpPort;
            propBag["typeFilterLevel"] = TypeFilterLevel.Full;
            propBag["name"] = Guid.NewGuid().ToString();
            if (isSecure)
            {
                propBag["secure"] = isSecure;
                propBag["impersonate"] = false;
            }
            return new TcpChannel(propBag, null, serverProv);
        }
    }
}