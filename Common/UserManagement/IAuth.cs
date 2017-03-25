namespace Common
{
    public interface IAuth
    {
        bool register(string _username, string _name, string _password);
        bool login(string _username, string _password);
    }
}