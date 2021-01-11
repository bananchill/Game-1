namespace Assets.Scrypts
{
    public class Message
    {
        public MessageType type;
        public string data;
        public string xml;

        public Message() { }

        public Message(MessageType type)
        {
            this.type = type;
            xml = Converter.StrignToXml(type, data);
        }

        public Message(MessageType type, string data)
        {
            this.type = type;
            this.data = data;
            xml = Converter.StrignToXml(type, data);
        }

        public MessageType Type()
        {
            return type;
        }

        public string Data()
        {
            return data;
        }

        public string Xml()
        {
            return xml;
        }
    }
}