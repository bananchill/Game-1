using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scrypts
{
    public class Main : MonoBehaviour
    {
        public InputField input;
        public Text nickname, gold, crystal;
        ClientServer client;

        public void Start()
        {
            client = new ClientServer();
            client.StartClient();
            //Account.CheckCharacter();
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
