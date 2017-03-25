using System;

namespace Common
{
    public class Message
    {
        private String source, destination, content;

        public Message(String _source, String _destination, String _content)
        {
            source = _source;
            destination = _destination;
            content = _content;
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
    }
}