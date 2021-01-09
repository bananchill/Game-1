using System;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scrypts
{
    public class Account : MonoBehaviour
    {
        public static Character character;
        public static Thread threadWail;

        public void CheckCharacter()
        {
            Read();
            if (character == null)
            {
                SceneManager.LoadSceneAsync("Registration");
                return;
            }
            ClientServer.Entry(character);
            threadWail = new Thread(new ThreadStart(Wait));
            threadWail.Start();
        }

        private static void Wait()
        {
            while (true)
            {
                try
                {
                    Thread.Sleep(1000);
                    if (character.Nickname() == null)
                    {
                        Debug.Log("No");
                    }
                    else
                    {
                        threadWail.Abort();
                        return;
                    }
                }
                catch (Exception) { }
            }
        }

        public static void Save(Character newCharacter)
        {
            PlayerPrefs.SetString("Mail", newCharacter.Mail());
            PlayerPrefs.SetString("Password", newCharacter.Password());
        }

        private void Read()
        {
            try
            {
                string mail = PlayerPrefs.GetString("Mail");
                string password = PlayerPrefs.GetString("Password");
                character = new Character(mail, password);
            }
            catch (Exception)
            {
                SceneManager.LoadSceneAsync("Registration");
                return;
            }
        }
    }
}