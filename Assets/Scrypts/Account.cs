using System;
using System.IO;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scrypts
{
    public class Account : MonoBehaviour
    {
        public static Character character;
        private static ClientServer client;
        private static string path;

        public static void CheckCharacter()
        {
            path = "/Character.json";
            if (File.Exists(path)) LoadCharacter();
            else SceneManager.LoadSceneAsync("Registration");
        }

        public static void LoadCharacter()
        {
            client = new ClientServer();
            character = JsonUtility.FromJson<Character>(File.ReadAllText(Application.streamingAssetsPath + path));
            if(character == null)
            {
                SceneManager.LoadSceneAsync("Registration");
                return;
            }
            client.GetAccount(character.Mail(), character.Password());
            Thread threadWail = new Thread(new ThreadStart(Wait));
            threadWail.Start();
        }

        private static void Wait()
        {
            while (true)
            {
                try
                {
                    if (character.Nickname() == null)
                    {
                        Thread.Sleep(300);
                    }
                    else
                    {
                        Debug.Log("Success! Nickname - "  + character.Nickname());
                        return;
                    }
                }
                catch (Exception) { }
            }
        }
    }
}