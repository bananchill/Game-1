using System;

namespace Assets
{
    [Serializable]
    public class Character
    {
        private string nickname;
        private string mail;
        private string password;

        public Character(string nickname, string mail, string password)
        {
            this.nickname = nickname;
            this.mail = mail;
            this.password = password;
        }

        public Character(string nickname, string password)
        {
            this.nickname = nickname;
            this.password = password;
        }

        public string Nickname
        {
            get
            {
                return nickname;
            }

            set
            {
                nickname = value;
            }
        }

        public string Mail
        {
            get
            {
                return mail;
            }

            set
            {
                mail = value;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
            }
        }
    }
}
