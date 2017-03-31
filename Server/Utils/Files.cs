using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml.Linq;

namespace Server.Utils
{
    public static class Files
    {
        public static string filename = "./users.xml";

        public static void Save(string _filename, List<User> _users)
        {
            try
            {
                var file = new XElement("Users",
                    from user in _users
                    select new XElement("User",
                        new XAttribute("ID", _users.IndexOf(user)),
                        new XElement("Name", user.name),
                        new XElement("Username", user.username),
                        new XElement("Password", user.password),
                        new XElement("Online", user.online),
                        new XElement("Port", user.port)));

                using (var mutex = new Mutex(false, "Users"))
                {
                    mutex.WaitOne();
                    file.Save(_filename);
                    mutex.ReleaseMutex();
                }

                Console.WriteLine("Users's file updated.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static List<User> Load(string _filename)
        {
            if (!File.Exists(_filename))
                return new List<User>();

            XDocument file;

            using (var mutex = new Mutex(false, "Users"))
            {
                mutex.WaitOne();
                file = XDocument.Load(_filename);
                mutex.ReleaseMutex();
            }

            List<User> users =
                file.Root
                .Elements("User")
                .Select(_user => new User((string)_user.Element("Username"),
                    (string)_user.Element("Name"),
                    (string)_user.Element("Password"),
                    (int)_user.Element("Port"),
                    (bool)_user.Element("Online"))).ToList();

            return users;
        }
    }
}