using Common;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Runtime.Remoting;

namespace Server
{
    public class Messages : MarshalByRefObject, IMessages
    {
        public event Handler onChange;
        private List<Message> messages;

        public override ObjRef CreateObjRef(Type requestedType)
        {
            messages = new List<Message>();
            return base.CreateObjRef(requestedType);
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public List<Message> getMessages()
        {
            return messages;
        }

        public void send(Message _message)
        {
            messages.Add(_message);
            broadcast(messages);
        }

        private void broadcast(List<Message> messages)
        {
            if (onChange != null)
            {
                Delegate[] invkList = onChange.GetInvocationList();

                foreach (Handler handler in invkList)
                {
                    new Thread(() =>
                    {
                        try
                        {
                            handler(messages);
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