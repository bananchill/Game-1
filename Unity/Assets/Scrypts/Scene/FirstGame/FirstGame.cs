using Assets.Scrypts;
using System;
using UnityEngine;

public class FirstGame : MonoBehaviour
{
    void Start()
    {
        GameServer game = new GameServer();
        game.StartClient("localhost", 3001);
        game.ConnectToServer();
        game.StartMain();
    }

    public static void SetInfo(Message message)
    {
        Debug.Log("SetInfo");
        string[] data = message.Data().Split('#');

        string mail = data[0];
        string password = data[1];
        Debug.Log(mail);
        Debug.Log(password);
    }

    public static void WaitTheGame()
    {
        Debug.Log("Loading game, please wait");
    }
}
