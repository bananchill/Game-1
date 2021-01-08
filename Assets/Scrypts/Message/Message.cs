namespace Assets.Scrypts
{
    public class Message
    {
        public MessageType type;
        public string dataFirst;
        public string dataSecond;
        public string dataThird;
        public string dataFourth;
        public string dataFifth;
        public string dataSixth;
        public string xml;

        public Message() { }

        public Message(MessageType type)
        {
            this.type = type;
            dataFirst = null;
            xml = Converter.StrignToXml(type, dataFirst);
        }

        public Message(MessageType type, string dataFirst)
        {
            this.type = type;
            this.dataFirst = dataFirst;
            xml = Converter.StrignToXml(type, dataFirst);
        }

        public Message(MessageType type, string identifier, string password)
        {
            this.type = type;
            dataFirst = identifier;
            dataSecond = password;
            xml = Converter.StrignToXml(type, dataFirst, dataSecond);
        }

        public Message(MessageType type, string nickname, string password, string mail)
        {
            this.type = type;
            dataFirst = nickname;
            dataSecond = password;
            dataThird = mail;
            xml = Converter.StrignToXml(type, dataFirst, dataSecond, dataThird);
        }

        public Message(MessageType type, string id, string nickname, string password, string mail, string gold, string crystal)
        {
            this.type = type;
            dataFirst = id;
            dataSecond = nickname;
            dataThird = password;
            dataFourth = mail;
            dataFifth = gold;
            dataSixth = crystal;
            xml = Converter.StrignToXml(type, dataFirst, dataSecond, dataThird, dataFourth, dataFifth, dataSixth);
        }

        public MessageType Type()
        {
            return type;
        }

        public string DataFirst()
        {
            return dataFirst;
        }

        public string DataSecond()
        {
            return dataSecond;
        }

        public string DataThird()
        {
            return dataThird;
        }

        public string DataFourth()
        {
            return dataFourth;
        }

        public string DataFifth()
        {
            return dataFifth;
        }

        public string DataSixth()
        {
            return dataSixth;
        }

        public string Xml()
        {
            return xml;
        }
    }
}
