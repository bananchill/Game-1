using System.Collections.Generic;
using UnityEngine;

public struct Card
{
    public string Name;
    public Sprite Logo;
    public int Attack, Defense;

    public Card(string name, string logoPath, int attack, int defense)
    {
        Name = name;
        Logo = Resources.Load<Sprite>(logoPath);
        Attack = attack;
        Defense = defense;
    }
}

public static class CardManagerList
{
    public static List<Card> allCards = new List<Card>();
}

public class CardManager : MonoBehaviour
{
    public void Awake()
    {
        CardManagerList.allCards.Add(new Card("wolf", "Sprites/Cards/wolf", 3, 3));
        CardManagerList.allCards.Add(new Card("wolf", "Sprites/Cards/wolf", 3, 3));
        CardManagerList.allCards.Add(new Card("wolf", "Sprites/Cards/wolf", 3, 3));
        CardManagerList.allCards.Add(new Card("wolf", "Sprites/Cards/wolf", 3, 3));
    }
}