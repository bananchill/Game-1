using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scrypts
{
    public class Main : MonoBehaviour
    {
        public InputField input;
        public Text nickname, gold, crystal;
        ClientServer client;
        public GameObject authorizationMenuUI;
        Thread threadAuth;

        public void Start()
        {
            client = new ClientServer();
            Debug.Log("Start app, client = " + ClientServer.online);

            client.StartClient();
            if (!Account.CheckCharacter())
            {
                authorizationMenuUI.SetActive(true);
                threadAuth = new Thread(new ThreadStart(WaitAuth));//не работает, не понимаю как скрыть форму и заполнить поля данными
                threadAuth.Start();
                //authorizationMenuUI.SetActive(false);
            }
            else
            {
                nickname.text = Account.character.Nickname();
                gold.text = Account.character.Gold().ToString();
                crystal.text = Account.character.Crystal().ToString();
                client.StartMain();
            }
        }

        private void WaitAuth()
        {
            while (true)
            {
                try
                {
                    Thread.Sleep(1000);
                    if (Account.character.Nickname() == null)
                    {
                        Debug.Log("No message with character");
                    }
                    if(Account.character.Nickname() != null)
                    {
                        nickname.text = Account.character.Nickname();
                        gold.text = Account.character.Gold().ToString();
                        crystal.text = Account.character.Crystal().ToString();
                        client.StartMain();
                        threadAuth.Abort();
                    }
                }
                catch (Exception) { }
            }            
        }

        public void SendMessage()
        {
            if (ClientServer.clientConnected)
            {
                string message;
                message = input.text;
                client.SendTextMessage(new Message(MessageType.TEXT, message));
            }
            else
            {
                ConsoleHelper.WriteMessage("Произошла ошибка во время работы клиента.");
            }
        }

        public void StartGame()
        {
            client.SendTextMessage(new Message(MessageType.GAME));
            GameServer gameServer = new GameServer();
            gameServer.goGame();
        }
    }
}
