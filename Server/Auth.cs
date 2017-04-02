using Common;
using Common.Utils;
using Server.Utils;
using System;
using System.Collections.Generic;
using System.Runtime.Remoting;
using System.Threading;

namespace Server
{
    class Auth : MarshalByRefObject, IAuth
    {
        public event UserHandler onChange;

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

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public List<User> Users()
        {
            return users;
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
                _user.port = 1024 + users.Count;
                users.Add(_user);

                Console.WriteLine("Success!");

                // Save updated users' list to file
                Files.Save(Files.filename, users);

                broadcast(users);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public User login(string _username, string _password, IP _ip)
        {
            // Temporary object for the user
            User test_user = new User(_username, _password, _ip);

            // Checks if the user exists in the List (uses Equals to compare)
            if (users.Exists(user => user.Equals(test_user) && !user.online))
            {
                User _user = users.Find(user => user.Equals(test_user));
                if (_user.password.Equals(test_user.password))
                {
                    _user.online = true;
                    _user.ip = _ip;
                    broadcast(users);
                    return _user;
                }
                else
                {
                    Console.WriteLine("Login failed.\n");
                    return null;
                }
            
            }
            else
            {
                Console.WriteLine("Login failed.\n");
                return null;
            }
        }

        public bool logout(string _username)
        {
            User _user = new User(_username, "");
            if (users.Exists(user => user.Equals(_user) && user.online))
            {
                users.Find(user => user.Equals(_user)).online = false;
                Console.Write(_user.username);
                Console.WriteLine(" exited the General ChatRoom!");
                broadcast(users);
                return true;
            }
            else
            {
                Console.WriteLine("Logout failed.\n");
                return false;
            }
        }

        private void broadcast(List<User> users)
        {
            Files.Save(Files.filename, users);

            if (onChange != null)
            {
                Delegate[] invkList = onChange.GetInvocationList();

                foreach (UserHandler handler in invkList)
                {
                    new Thread(() =>
                    {
                        try
                        {
                            handler(users);
                            Console.WriteLine("Invoking event handler");
                        }
                        catch (Exception)
                        {
                            onChange -= handler;
                            Console.WriteLine("Exception: Removed an event handler");
                        }
                    }).Start();
                }
            }
        }
    }
}