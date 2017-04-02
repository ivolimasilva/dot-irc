using System;

namespace Common
{
    [Serializable]
    public class Message
    {
        private string source, destination, content;
        private bool end;

        public Message(string _source, string _destination, string _content, bool _end)
        {
            source = _source;
            destination = _destination;
            content = _content;
            end = _end;
        }

        public Message(string _source, string _destination, string _content)
        {
            source = _source;
            destination = _destination;
            content = _content;
            end = false;
        }

        public Message(string _source, string _destination, bool _end)
        {
            source = _source;
            destination = _destination;
            end = true;
        }

        public string Source()
        {
            return source;
        }

        public string Destination()
        {
            return destination;
        }

        public string Content()
        {
            return content;
        }

        public bool End()
        {
            return end;
        }
    }
}