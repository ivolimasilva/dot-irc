using System;

namespace Common
{
    [Serializable]
    public class Message
    {
        private String source, destination, content;
        private bool end;

        public Message(String _source, String _destination, String _content, bool _end)
        {
            source = _source;
            destination = _destination;
            content = _content;
            end = _end;
        }

        public Message(String _source, String _destination, String _content)
        {
            source = _source;
            destination = _destination;
            content = _content;
            end = false;
        }

        public Message(String _source, String _destination, bool _end)
        {
            source = _source;
            destination = _destination;
            end = true;
        }

        public String Source()
        {
            return source;
        }

        public String Destination()
        {
            return destination;
        }

        public String Content()
        {
            return content;
        }

        public bool End()
        {
            return end;
        }
    }
}