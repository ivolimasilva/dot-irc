using Client.Views;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Client
{
    public class Requests : MarshalByRefObject, IRequests
    {
        public bool ask(User _source, User _destination)
        {
            DialogResult dialogResult = MessageBox.Show("Request from " + _source.name + " (" + _source.ip + ":" + _source.port + ")?", "Start dialog?", MessageBoxButtons.OKCancel);

            if (dialogResult == DialogResult.OK)
            {
                new Thread(() =>
                {
                    Application.Run(new ChatRoom(_destination, _source));
                }).Start();
            }

            return dialogResult == DialogResult.OK;
        }
    }
}