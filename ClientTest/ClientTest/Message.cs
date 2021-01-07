namespace ClientTest
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
