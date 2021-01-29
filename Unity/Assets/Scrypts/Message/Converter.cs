using System.IO;
using System.Xml.Serialization;

namespace Assets.Scrypts
{
    public class Converter
    {
        public static string StrignToXml(MessageType type, string data)
        {
            string xml = "<Message><type>" + type.ToString() + "</type><data>" + data + "</data></Message>";
            return xml;
        }

        public static string CharacterToXml(Character account)
        {
            string serializeCharacter =
                $"<character>" +
                $"<id>{account.Id()}</id>" +
                $"<nickname>{account.Nickname()}</nickname>" +
                $"<password>{account.Password()}</password>" +
                $"<mail>{account.Mail()}</mail>" +
                $"<gold>{account.Gold()}</gold>" +
                $"<crystal>{account.Crystal()}</crystal>" +
                $"</character>";
            return serializeCharacter;
        }

        public static Message XmlToObject(string s)
        {
            Message message;
            var serializer = new XmlSerializer(typeof(Message));
            using (var xmlStream = new StringReader(s))
            {
                message = (Message)serializer.Deserialize(xmlStream);
            }
            return message;
        }

        public static Chest XmlToChest(string s)
        {
            Chest message;
            var serializer = new XmlSerializer(typeof(Chest));
            using (var xmlStream = new StringReader(s))
            {
                message = (Chest)serializer.Deserialize(xmlStream);
            }
            return message;
        }

        public static EnemyBot XmlToEnemy(string s)
        {
            EnemyBot message;
            ConsoleHelper.WriteMessage(s + " enemyyyy");
            var serializer = new XmlSerializer(typeof(EnemyBot));
            using (var xmlStream = new StringReader(s))
            {
                message = (EnemyBot)serializer.Deserialize(xmlStream);
            }
            return message;
        }
    }
}
