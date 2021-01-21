using Assets.Scrypts;
using System;
using UnityEngine;

public class FirstGame : MonoBehaviour
{
    void Start()
    {
        GameServer game = new GameServer();
    }

    public static void SetInfo(Message message)
    {
        Debug.Log("SetInfo");
    }

    public static void WaitTheGame()
    {
        Debug.Log("Loading game, please wait");
    }
}
