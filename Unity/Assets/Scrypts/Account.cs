using System;
using UnityEngine;

namespace Assets.Scrypts
{
    public class Account : MonoBehaviour
    {
        public static Character character;

        public static bool CheckCharacter()
        {
            //if (Read())
            //{
            //    //if (character.Mail().Length < 2 || character.Password().Length < 8)
            //    //{
            //    //    return false;
            //    //}
            //    //ClientServer.Entry(character);
            //}

            character = new Character("keosha@mail.ru", "password1234");
            ClientServer.Entry(character);

            return true;
        }

        public static void Save(Character newCharacter)
        {
            PlayerPrefs.SetString("Mail", newCharacter.mail);
            PlayerPrefs.SetString("Password", newCharacter.password);
            Debug.Log("Saved character in file");
        }

        private static bool Read()
        {
            try
            {
                string mail = PlayerPrefs.GetString("Mail");
                string password = PlayerPrefs.GetString("Password");
                character = new Character(mail, password);
                Debug.Log("Checked character in file : mail - " + mail + ", password - " + password);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}