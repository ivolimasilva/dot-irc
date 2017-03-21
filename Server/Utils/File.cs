using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;

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
                        new XElement("Password", user.password)));

                file.Save(_filename);
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

            XDocument file = XDocument.Load(_filename);

            List<User> users =
                file.Root
                .Elements("User")
                .Select(_user => new User((string)_user.Element("Username"), (string)_user.Element("Name"), (string)_user.Element("Password"))).ToList();

            return users;
        }
    }
}