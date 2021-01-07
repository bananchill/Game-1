using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace Assets
{
    class Registration : MonoBehaviour
    {
        private Character character;
        private string path;
        private FileStream fileStream;
        public InputField nickname, mail, password;
            
        public void RegistrationNewCharacter()
        {
            path = "/Character.json";
            fileStream = File.Create(path);
            character = new Character(nickname.text, mail.text, password.text);
            Save(character);
        }

        private void Save(Character newCharacter)
        {
            File.WriteAllText(Application.streamingAssetsPath + path, JsonUtility.ToJson(newCharacter));
        }
    }
}
