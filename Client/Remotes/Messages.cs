using Client.Utils;
using Common;
using System;
using System.Collections.Generic;

namespace Client.Remotes
{
    class Messages : IMessages
    {
        public event Handler onChange;
        private IMessages remoteMessages;

        public Messages()
        {
            string url = Remoting.GetApplicationURL(typeof(IMessages));
            remoteMessages = (IMessages)Activator.GetObject(typeof(IMessages), url);
        }

        public List<Message> getMessages()
        {
            return remoteMessages.getMessages();
        }

        public void send(Message _message)
        {
            remoteMessages.send(_message);
        }
    }
}
