using System;

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

        public Character() { }

        public Character(string mail, string password)
        {
            this.mail = mail;
            this.password = password;
        }

        public Character(string mail, string password, string nickname)
        {
            this.mail = mail;
            this.nickname = nickname;
            this.password = password;
        }

        public Character(int id, string nickname, string password, string mail, int gold, int crystal)
        {
            this.id = id;
            this.nickname = nickname;
            this.password = password;
            this.mail = mail;
            this.gold = gold;
            this.crystal = crystal;
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
    }
}
