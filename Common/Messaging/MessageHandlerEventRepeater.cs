using System;
using System.Collections.Generic;

namespace Common
{
    public class MessageHandlerEventRepeater : MarshalByRefObject
    {
        public event MessageHandler onChange;

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public void Repeater(List<Message> messages)
        {
            onChange?.Invoke(messages);
        }
    }
}