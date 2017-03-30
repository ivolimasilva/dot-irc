using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Xml.Linq;

namespace Client
{
    class PrivateMessages : MarshalByRefObject, IPrivateMessages
    {
        private EventWaitHandle waitHandle = new EventWaitHandle(true, EventResetMode.AutoReset, "SHARED_BY_ALL_PROCESSES");

        private string filename;

        private List<Message> messages = new List<Message>();

        public void send(Message _message)
        {
            // Save Messages to a file
            try
            {
                filename = "./messages-" + _message.Destination() + ".xml";

                LoadMessages(_message.Destination());

                messages.Add(_message);

                XElement file = new XElement("Messages",
                    from message in messages
                    select new XElement("Message",
                    new XAttribute("ID", messages.IndexOf(message)),
                    new XElement("Source", message.Source()),
                    new XElement("Destination", message.Destination()),
                    new XElement("Content", message.Content()),
                    new XElement("End", message.End())));

                using (var mutex = new Mutex(false, "Message" + _message.Destination()))
                {
                    mutex.WaitOne();
                    file.Save(filename);
                    mutex.ReleaseMutex();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void LoadMessages(String _username)
        {
            messages.Clear();

            XDocument file;
            using (var mutex = new Mutex(false, "Message" + _username))
            {
                mutex.WaitOne();
                file = XDocument.Load(filename);
                mutex.ReleaseMutex();
            }

            // Load the list with all messages
            messages =
                file.Root
                .Elements("Message")
                .Select(_message => new Common.Message(
                    (string)_message.Element("Source"),
                    (string)_message.Element("Destination"),
                    (string)_message.Element("Content"),
                    (bool)_message.Element("End"))).ToList();

            // Remove messages from another conversations
            // messages.RemoveAll(_message => _message.Destination() != _username && _message.Source() != _username);
        }
    }
}