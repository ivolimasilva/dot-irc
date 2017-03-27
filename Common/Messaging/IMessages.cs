using System.Collections.Generic;

namespace Common
{
    public delegate void MessageHandler(List<Message> _messages);

    public interface IMessages
    {
        event MessageHandler onChange;
        void send(Message _message);
    }
}