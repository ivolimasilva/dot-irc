using Client.Utils;
using Common;
using System;

namespace Client.Remotes
{
    class Messages : IMessages
    {
        public event MessageHandler onChange;
        private IMessages remoteMessages;

        public Messages()
        {
            string url = Remoting.GetApplicationURL(typeof(IMessages));
            remoteMessages = (IMessages)Activator.GetObject(typeof(IMessages), url);
        }

        public void send(Message _message)
        {
            remoteMessages.send(_message);
        }
    }
}