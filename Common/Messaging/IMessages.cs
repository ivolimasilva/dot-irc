using System;
using System.Collections.Generic;

namespace Common
{
    public delegate void Handler(List<Message> _messages);

    public interface IMessages
    {
        event Handler onChange;
        List<Message> getMessages();
        void send(Message _message);
    }

    
}