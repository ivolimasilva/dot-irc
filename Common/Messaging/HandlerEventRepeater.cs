using System;
using System.Collections.Generic;

namespace Common
{
    public class HandlerEventRepeater : MarshalByRefObject
    {
        public event Handler onChange;

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
