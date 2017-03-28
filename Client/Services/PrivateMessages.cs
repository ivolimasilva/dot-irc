using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Client
{
    class PrivateMessages : MarshalByRefObject, IPrivateMessages
    {
        private static string filenameExt = ".xml";

        private List<Message> messages = new List<Message>();

        public void send(Message _message)
        {
            // Add new message to array
            messages.Add(_message);

            // Save Messages to a file
            try
            {
                string filename = "./messages-" + _message.Destination() + filenameExt;

                XElement file = new XElement("Messages",
                    from message in messages
                    select new XElement("Message",
                    new XAttribute("ID", messages.IndexOf(message)),
                    new XElement("Source", message.Source()),
                    new XElement("Destination", message.Destination()),
                    new XElement("Content", message.Content())));

                file.Save(filename);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}