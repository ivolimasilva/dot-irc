using System.Collections.Generic;

namespace Common
{
    public delegate void UserHandler(List<User> users);

    public interface IAuth
    {
        event UserHandler onChange;
        bool register(string _username, string _name, string _password);
        User login(string _username, string _password, Utils.IP _ip);
        bool logout(string _username);
        List<User> Users();
    }
}