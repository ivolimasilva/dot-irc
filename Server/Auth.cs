using System;
using Common;
using System.Collections.Generic;

namespace Server
{
    class Auth : MarshalByRefObject, IAuth
    {
        private static List<User> users = new List<User>();

        public bool register(string _username, string _name, string _password)
        {
            // Temporary object for the new user
            User _user = new User(_username, _name, _password);

            Console.WriteLine("Registering:");
            Console.WriteLine(_user);

            // Checks if the new user already exists in the List (uses Equals to compare)
            if (users.Exists(user => user.Equals(_user)))
            {
                Console.WriteLine("Already exists!");
                return false;
            }

            // Adds the new user to the List
            try
            {
                users.Add(_user);
                Console.WriteLine("Success!");
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool login(string _username, string _password)
        {
            // Temporary object for the user
            User _user = new User(_username, _password);

            // Checks if the user exists in the List (uses Equals to compare)
            if (users.Exists(user => user.Equals(_user)))
            {
                return true;
            }

            return false;
        }
    }
}