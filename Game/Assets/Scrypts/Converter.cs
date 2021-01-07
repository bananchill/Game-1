namespace Assets.Scrypts
{
    public class Converter
    {
        //static JavaScriptSerializer serializer;

        //public static string ToXml(Message message)
        //{
        //    serializer = new JavaScriptSerializer();
        //    var json = serializer.Serialize(message);
        //    return json;
        //}

        //public static Message ToMessage(string xml)
        //{
        //    Message message;
        //    serializer = new JavaScriptSerializer();
        //    message = serializer.Deserialize<Message>(xml);
        //    return message;
        //}

        public static string StrignToXml(MessageType type, string data)
        {
            string xml = "<Message><type>" + type.ToString() + "</type><data>" + data + "</data></Message>";
            return xml;
        }
    }
}
