using System;

namespace Common
{
    [Serializable]
    public class User
    {
        public string username { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public bool online { get; set; }
        public Utils.IP ip { get; set; }
        public int port { get; set; }

        public User(string _username, string _name, string _password, int _port)
        {
            username = _username;
            name = _name;
            password = _password;
            port = _port;
            online = false;
        }

        public User(string _username, string _name, string _password)
        {
            username = _username;
            name = _name;
            password = _password;
            online = false;
        }

        public User(string _username, string _password, Utils.IP _ip)
        {
            username = _username;
            password = _password;
            ip = _ip;
            online = false;
        }

        public User(string _username, string _password)
        {
            username = _username;
            password = _password;
            online = false;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            User objAsUser = obj as User;

            if (objAsUser == null)
                return false;
            else
                return Equals(objAsUser);
        }

        public bool Equals(User _user)
        {
            if (this.username == _user.username)
                return true;
            else
                return false;
        }

        public override string ToString()
        {
            string _tmp = "Username:\t" + this.username + "\nName:\t\t" + this.name;
            return _tmp;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}