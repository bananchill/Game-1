namespace Assets.Scrypts
{
    public class Message
    {
        private MessageType type;
        private string data;
        private string xml;

        public Message() { }

        public Message(MessageType type)
        {
            this.type = type;
            data = null;
            xml = Converter.StrignToXml(type, data);
        }

        public Message(MessageType type, string data)
        {
            this.type = type;
            this.data = data;
            xml = Converter.StrignToXml(type, data);
        }

        public MessageType getType()
        {
            return type;
        }

        public string getData()
        {
            return data;
        }

        public string getXml()
        {
            return xml;
        }

        public override string ToString()
        {
            return "Message{" +
                "type=" + type +
                ", data='" + data + '\'' +
                ", xml='" + xml + '\'' +
                '}';
        }
    }
}
