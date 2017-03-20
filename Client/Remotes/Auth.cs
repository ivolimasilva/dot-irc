using System;
using Common;
using Client.Utils;

namespace Client.Remotes
{
    class Auth : IAuth
    {
        private IAuth remoteAuth;

        public Auth()
        {
            string url = Remoting.GetApplicationURL(typeof(IAuth));
            remoteAuth = (IAuth)Activator.GetObject(typeof(IAuth), url);
        }

        public bool login(string _username, string _password)
        {
            return remoteAuth.login(_username, _password);
        }

        public bool register(string _username, string _name, string _password)
        {
            return remoteAuth.register(_username, _name, _password);
        }
    }
}