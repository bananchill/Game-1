using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scrypts
{
    public class Main : MonoBehaviour
    {
        public InputField input;
        public Text nickname, gold, crystal;
        ClientServer client;
        Account account;

        public void Start()
        {
            if (client == null)
            {
                client = new ClientServer();
                account = new Account();

                client.StartClient();
                account.CheckCharacter();
                nickname.text = Account.character.Nickname();
                gold.text = Account.character.Gold().ToString();
                crystal.text = Account.character.Crystal().ToString();
                client.StartMain();
            }
        }

        public void SendMessage()
        {
            if (client.clientConnected)
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
    }
}
