using System;
using Common;
using Server.Utils;
using System.Collections.Generic;
using System.Runtime.Remoting;

namespace Server
{
    class Auth : MarshalByRefObject, IAuth
    {
        private static List<User> users = new List<User>();

        public override ObjRef CreateObjRef(Type requestedType)
        {
            // Load users' list from file
            users = Files.Load(Files.filename);

            Console.WriteLine("Loaded Users:");
            users.ForEach(delegate (User user)
            {
                Console.WriteLine(user);
            });

            return base.CreateObjRef(requestedType);
        }

        public bool register(string _username, string _name, string _password)
        {
            // Temporary object for the new user
            User _user = new User(_username, _name, _password);

            Console.WriteLine("Registering:");
            Console.WriteLine(_user);

            // Checks if the new user already exists in the List (uses Equals to compare)
            if (users.Exists(user => user.Equals(_user)))
            {
                Console.WriteLine("Username already exists!\n");
                return false;
            }

            // Adds the new user to the List
            try
            {
                users.Add(_user);
<<<<<<< HEAD
                Console.WriteLine("Success!");

                // Save updated users' list to file
                Files.Save(Files.filename, users);

=======
                Console.WriteLine("Success!\n");                
>>>>>>> e14580aefe01835ed7aa943aaf5c3bffcaa34a6c
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
            if (users.Exists(user => user.Equals(_user) && !user.online))
            {
                users.Find(user => user.Equals(_user)).online = true;                
                Console.Write(_user.username);
                Console.WriteLine(" entered the General ChatRoom!");
                Console.WriteLine("Success!\n");                
                return true;
            }
            else
            {
                Console.WriteLine("Login failed.\n");
                return false;
            }            
        }
    }
}