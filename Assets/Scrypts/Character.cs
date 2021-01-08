using System;

namespace Assets.Scrypts
{
    [Serializable]
    public class Character
    {
        private int id;
        private string nickname;
        private string password;
        private string mail;
        private int gold;
        private int crystal;

        public Character(string nickname, string password, string mail)
        {
            this.nickname = nickname;
            this.password = password;
            this.mail = mail;
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
