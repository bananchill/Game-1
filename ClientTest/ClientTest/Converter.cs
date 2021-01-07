using Nancy.Json;

namespace ClientTest
{
    public class Converter
    {
        //public static void Main(string[] args)
        //{

        //}

        static JavaScriptSerializer serializer;

        public static string ToXml(Message message)
        {
            serializer = new JavaScriptSerializer();
            var json = serializer.Serialize(message);
            ConsoleHelper.writeMessage(json + " ToXml");
            return json;
        }

        public static Message ToMessage(string xml)
        {
            Message message;
            serializer = new JavaScriptSerializer();
            ConsoleHelper.writeMessage(xml + " ToMessage");
            message = serializer.Deserialize<Message>(xml);
            return message;
        }

        public static string StrignToXml(MessageType type, string data)
        {
            string xml = "<Message><type>" + type.ToString() + "</type><data>" + data + "</data></Message>";
            return xml;
        }
    }
}
