using System.Collections.Generic;
using UnityEngine;

public class Card//maincamera success
{
    public string Name;
    public Sprite Logo { get; set; }
    public int Attack, Defense;
    public bool CanAttack;
    public bool IsPlaced;

    public bool IsAlive
    {
        get
        {
            return Defense > 0;
        }
    }

    public Card(string name, string logoPath, int attack, int defense)
    {
        Name = name;
        Logo = Resources.Load<Sprite>(logoPath);
        Attack = attack;
        Defense = defense;
        CanAttack = false;
        IsPlaced = false;
    }

    public Card(string name, int attack, int defense)
    {
        Name = name;
        Logo = null;
        Attack = attack;
        Defense = defense;
        CanAttack = false;
        IsPlaced = false;
    }

    public void ChangeAttackState(bool can)
    {
        CanAttack = can;
    }

    public void GetDamage(int damage)
    {
        Defense -= damage;
    }
}

public static class CardManagerList
{
    public static List<Card> allCards = new List<Card>();
}

public class CardManagerScr : MonoBehaviour
{
    public void Awake()
    {
        foreach(Card card in CardManagerList.allCards)
        {
            card.Logo = Resources.Load<Sprite>("Sprites/Cards/" + card.Name);
        }
        //CardManagerList.allCards.Add(new Card("wolf", "Sprites/Cards/wolf", 3, 3));
        //CardManagerList.allCards.Add(new Card("parrot", "Sprites/Cards/parrot", 2, 1));
        //CardManagerList.allCards.Add(new Card("snake", "Sprites/Cards/snake", 1, 6));
        //CardManagerList.allCards.Add(new Card("squirrel", "Sprites/Cards/squirrel", 5, 2));
    }
}