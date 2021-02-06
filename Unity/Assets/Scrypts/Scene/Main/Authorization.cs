using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scrypts
{
    public class Authorization : MonoBehaviour
    {
        private static Character character;

        public InputField mail, password, nickname;

        public Authorization() { }

        public void RegistrationNewCharacter()
        {
            character = new Character(mail.text, password.text, nickname.text);
            Account.Save(character);
            ClientServer.AddCharacter(character);
        }

        public void Entry()
        {
            character = new Character(mail.text, password.text);
            Account.Save(character);
            ClientServer.Entry(character);
            Debug.Log(Client.clientConnected + " status client");
            //Main.authorizationMenuUI.SetActive(false);
        }
    }
}