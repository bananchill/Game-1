namespace Assets.Scrypts
{
    public class Message
    {
        public MessageType type { get; set; }
        public string data { get; set; }
        public string xml { get; set; }

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
    }
}