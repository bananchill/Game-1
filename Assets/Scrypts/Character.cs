using System;
using System.Xml.Serialization;

namespace Assets.Scrypts
{
    [Serializable]
    public class Character
    {
        [NonSerialized]
        private int id;

        [NonSerialized]
        private string nickname;

        private string mail;

        private string password;

        [NonSerialized]
        private int gold;

        [NonSerialized]
        private int crystal;

        [NonSerialized]
        private string xml;

        public Character() { }
        public Character(string mail, string password)
        {
            this.mail = mail;
            this.password = password;
            xml = "<Character><mail>" + mail + "</mail><password>" + password + "</password></Character>";
        }

        public Character(string mail, string password, string nickname)
        {
            this.mail = mail;
            this.nickname = nickname;
            this.password = password;
            xml = "<Character><mail>" + mail + "</mail><password>" + password + "</password></Character>";
        }

        public Character(int id, string nickname, string password, string mail, int gold, int crystal)
        {
            this.id = id;
            this.nickname = nickname;
            this.password = password;
            this.mail = mail;
            this.gold = gold;
            this.crystal = crystal;
            xml = "<Character><mail>" + mail + "</mail><password>" + password + "</password></Character>";
        }

        public int Id()
        {
            return id;
        }

        public string Nickname()
        {
            return nickname;
        }

        public string Mail()
        {
            return mail;
        }

        public string Password()
        {
            return password;
        }

        public int Gold()
        {
            return gold;
        }

        public int Crystal()
        {
            return crystal;
        }

        public string Xml()
        {
            return xml;
        }
    }
}
