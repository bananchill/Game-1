using Assets;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Check : MonoBehaviour
{
    private Character character;
    private string path;

    void Start()
    {
        path = "/Character.json";
        if (File.Exists(path)) Load();
        else SceneManager.LoadSceneAsync("Registration");
    }

    public void Load()
    {
        character = JsonUtility.FromJson<Character>(File.ReadAllText(Application.streamingAssetsPath + path));
    }
}