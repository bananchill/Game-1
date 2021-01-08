using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scrypts
{
    class Registration : MonoBehaviour
    {
        private Character character;
        private string path;
        private FileStream fileStream;
        public InputField nickname, password, mail;
        ClientServer client;
            
        public void RegistrationNewCharacter()
        {
            path = "/Character.json";
            fileStream = File.Create(path);
            character = new Character(nickname.text, password.text, mail.text);
            Save(character);
            client.AddCharacter(character);
        }

        private void Save(Character newCharacter)
        {
            File.WriteAllText(Application.streamingAssetsPath + path, JsonUtility.ToJson(newCharacter));
        }
    }
}
