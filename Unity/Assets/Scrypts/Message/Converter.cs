namespace Assets.Scrypts
{
    public class Converter
    {
        public static string StrignToXml(MessageType type, string data)
        {
            string xml = "<Message><type>" + type.ToString() + "</type><data>" + data + "</data></Message>";
            return xml;
        }

        public static string StrignToXml(MessageType type, string identifier, string password)
        {
            string xml = "<Message><type>" + type.ToString() + "</type><identifier>" + identifier + "</identifier><password>" + password + "</password></Message>";
            return xml;
        }

        public static string StrignToXml(MessageType type, string identifier, string password, string mail)
        {
            string xml = "<Message><type>" + type.ToString() + "</type><identifier>" + identifier + "</identifier><password>" + password + "</password>" +
                "<mail>" + mail + "</mail></Message>";
            return xml;
        }

        public static string StrignToXml(MessageType type, string id, string nickname, string password, string mail, string gold, string crystal)
        {
            string xml = "<Message><type>" + type.ToString() + "</type><id>" + id + "</id><nickname>" + nickname + "</nickname>" +
                "<password>" + password + "</password><mail>" + mail + "</mail><gold>" + gold + "</gold><crystal>" + crystal + "</crystal></Message>";
            return xml;
        }
    }
}
