using System.Collections.Generic;

namespace Common
{
    public delegate void MessageHandler(List<Message> _messages);

    public interface IMessages
    {
        event MessageHandler onChange;
        List<Message> getMessages();
        void send(Message _message);
    }
}