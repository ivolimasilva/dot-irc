using System;
using Common;
using Client.Utils;
using System.Collections.Generic;
using Common.Utils;

namespace Client.Remotes
{
    class Auth : IAuth
    {
        private IAuth remoteAuth;

        public event UserHandler onChange;

        public Auth()
        {
            string url = Remoting.GetApplicationURL(typeof(IAuth));
            remoteAuth = (IAuth)Activator.GetObject(typeof(IAuth), url);
        }

        public User login(string _username, string _password, IP _ip)
        {
            return remoteAuth.login(_username, _password, _ip);
        }

        public bool register(string _username, string _name, string _password)
        {
            return remoteAuth.register(_username, _name, _password);
        }

        public bool logout(string _username)
        {
            return remoteAuth.logout(_username);
        }

        public List<User> Users()
        {
            return remoteAuth.Users();
        }
    }
}