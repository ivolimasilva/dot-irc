using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Common
{
    public class UserHandlerEventRepeater : MarshalByRefObject
    {
        public event UserHandler onChange;

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public void Repeater(List<User> users)
        {
            onChange?.Invoke(users);
        }
    }
}