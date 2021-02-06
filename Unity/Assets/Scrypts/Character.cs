using System;

namespace Assets.Scrypts
{
    [Serializable]
    public class Character
    {
        public int id { get; set; }

        public string nickname { get; set; }

        public string mail { get; set; }

        public string password { get; set; }

        public int gold { get; set; }

        public int health { get; set; }
        public float x { get; set; }
        public float z { get; set; }

        public Character() { }

        public Character(string mail, string password)
        {
            this.mail = mail;
            this.password = password;
            id = -1;
            nickname = null;
            gold = -1;
        }

        public Character(string mail, string password, string nickname)
        {
            this.mail = mail;
            this.nickname = nickname;
            this.password = password;
            id = -1;
            gold = -1;
        }

        public Character(int id, string nickname, string password, string mail, int gold)
        {
            this.id = id;
            this.nickname = nickname;
            this.password = password;
            this.mail = mail;
            this.gold = gold;
        }
    }
}
