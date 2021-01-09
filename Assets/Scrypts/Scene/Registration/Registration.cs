using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scrypts
{
    class Registration : MonoBehaviour
    {
        private Character character;
        public InputField nickname, password, mail;

        public void RegistrationNewCharacter()
        {
            character = new Character(mail.text, password.text, nickname.text);
            Account.Save(character);
            ClientServer.AddCharacter(character);
            SceneManager.LoadSceneAsync("Main");
        }

        public void Entry()
        {
            character = new Character(mail.text, password.text);
            Account.Save(character);
            ClientServer.Entry(character);
            SceneManager.LoadSceneAsync("Main");
        }
    }
}
