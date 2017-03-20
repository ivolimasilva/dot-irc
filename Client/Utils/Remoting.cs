using System;
using System.Runtime.Remoting;

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
    }
}
