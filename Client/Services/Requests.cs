using Common;
using System;
using System.Windows.Forms;

namespace Client
{
    public class Requests : MarshalByRefObject, IRequests
    {
        public bool ask(User _user)
        {
            DialogResult dialogResult = MessageBox.Show("Request from " + _user.name + " (" + _user.ip + ":" + _user.port + ")?", "Start dialog?", MessageBoxButtons.OKCancel);
            return dialogResult == DialogResult.OK;
        }
    }
}