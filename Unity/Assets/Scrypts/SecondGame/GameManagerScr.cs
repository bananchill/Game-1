﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game//canvas
{
    public List<Card> EnemyDeck, PlayerDeck;

    public Game()
    {
        EnemyDeck = GiveDeckCard();
        PlayerDeck = GiveDeckCard();
    }

    List<Card> GiveDeckCard()
    {
        List<Card> list = new List<Card>();
        for (int i = 0; i < 10; i++)
            list.Add(CardManagerList.allCards[Random.Range(0, CardManagerList.allCards.Count)]);
        return list;
    }
}

public class GameManagerScr : MonoBehaviour
{
    public Game CurrentGame;
    public Transform EnemyHand, PlayerHand,
                     EnemyField, PlayerFiend;
    public GameObject CardPref;
    int Turn, TurnTime = 30;
    public Text TurnTimeTxt;
    public Button EndTurnBtn;

    public int PlayerHP, EnemyHP;
    public Text PlayerHPText, EnemyHPText;

    public GameObject ResultGO;
    public Text ResultText;

    public List<CardInfoScr> PlayerHandCards = new List<CardInfoScr>(),
                          PlayerFieldCards = new List<CardInfoScr>(),
                          EnemyHandCards = new List<CardInfoScr>(),
                          EnemyFieldCards = new List<CardInfoScr>();

    public bool IsPlayerTurn
    {
        get
        {
            return Turn % 2 == 0;
        }
    }

    void Start()
    {
        Turn = 0;

        CurrentGame = new Game();

        GiveHandCards(CurrentGame.EnemyDeck, EnemyHand);
        GiveHandCards(CurrentGame.PlayerDeck, PlayerHand);

        PlayerHP = EnemyHP = 30;

        StartCoroutine(TurnFunc());
    }

    void GiveHandCards(List<Card> deck, Transform hand)
    {
        int i = 0;
        while (i++ < 4)
            GiveCardToHand(deck, hand);
    }

    void GiveCardToHand(List<Card> deck, Transform hand)
    {
        if (deck.Count == 0)
            return;

        Card card = deck[0];

        GameObject cardGO = Instantiate(CardPref, hand, false);

        if (hand == EnemyHand)
        {
            cardGO.GetComponent<CardInfoScr>().HideCardInfo(card);
            EnemyHandCards.Add(cardGO.GetComponent<CardInfoScr>());
        }
        else
        {
            cardGO.GetComponent<CardInfoScr>().ShowCardInfo(card, true);
            PlayerHandCards.Add(cardGO.GetComponent<CardInfoScr>());
            cardGO.GetComponent<AttackedCard>().enabled = false;
        }

        deck.RemoveAt(0);
    }

    IEnumerator TurnFunc()
    {
        TurnTime = 30;
        TurnTimeTxt.text = TurnTime.ToString();

        foreach (var card in PlayerFieldCards)
            card.DeHighLightCard();

        if (IsPlayerTurn)
        {
            foreach (var card in PlayerFieldCards)
            {
                card.SelfCard.ChangeAttackState(true);
                card.HighLightCard();
            }

            while (TurnTime-- > 0)
            {
                TurnTimeTxt.text = TurnTime.ToString();
                yield return new WaitForSeconds(1);
            }
        }
        else
        {
            foreach (var card in PlayerFieldCards)
                card.SelfCard.ChangeAttackState(true);

            while (TurnTime-- > 27)
            {
                TurnTimeTxt.text = TurnTime.ToString();
                yield return new WaitForSeconds(1);
            }

            if (EnemyHandCards.Count > 0)
                EnemyTurn(EnemyHandCards);
        }

        ChangeTurn();
    }

    void EnemyTurn(List<CardInfoScr> cards)
    {
        int count = cards.Count == 1 ? 1 : Random.Range(0, cards.Count);

        for (int i = 0; i < count; i++)
        {
            if (EnemyFieldCards.Count > 5)
                break;

            cards[0].ShowCardInfo(cards[0].SelfCard, false);
            cards[0].transform.SetParent(EnemyField);

            EnemyFieldCards.Add(cards[0]);
            EnemyHandCards.Remove(cards[0]);
        }

        foreach (var activeCard in EnemyFieldCards.FindAll(x => x.SelfCard.CanAttack))
        {
            if (PlayerFieldCards.Count == 0)
                return;

            var enemy = PlayerFieldCards[Random.Range(0, PlayerFieldCards.Count)];

            Debug.Log(activeCard.SelfCard.Name + " (" + activeCard.SelfCard.Attack + ";" + activeCard.SelfCard.Defense + ") --->" +
                enemy.SelfCard.Name + " (" + enemy.SelfCard.Attack + ";" + enemy.SelfCard.Defense + ")");

            activeCard.SelfCard.ChangeAttackState(false);
            CardsFight(enemy, activeCard);
        }
    }

    public void ChangeTurn()
    {
        StopAllCoroutines();
        Turn++;

        EndTurnBtn.interactable = IsPlayerTurn;

        if (IsPlayerTurn)
            GiveNewCards();

        StartCoroutine(TurnFunc());
    }

    void GiveNewCards()
    {
        GiveCardToHand(CurrentGame.EnemyDeck, EnemyHand);
        GiveCardToHand(CurrentGame.PlayerDeck, PlayerHand);
    }

    public void CardsFight(CardInfoScr playerCard, CardInfoScr enemyCard)
    {
        playerCard.SelfCard.GetDamage(enemyCard.SelfCard.Attack);
        enemyCard.SelfCard.GetDamage(playerCard.SelfCard.Attack);

        if (!playerCard.SelfCard.IsAlive)
            DesctoyCard(playerCard);
        else
            playerCard.RefreshData();

        if (!enemyCard.SelfCard.IsAlive)
            DesctoyCard(enemyCard);
        else
            enemyCard.RefreshData();
    }

    private void ShowHP()
    {
        EnemyHPText.text = EnemyHP.ToString();
        PlayerHPText.text = PlayerHP.ToString();
    }

    void DesctoyCard(CardInfoScr card)
    {
        card.GetComponent<CardMovemantScr>().OnEndDrag(null);

        if (EnemyFieldCards.Exists(x => x == card))
            EnemyFieldCards.Remove(card);

        if (PlayerFieldCards.Exists(x => x == card))
            PlayerFieldCards.Remove(card);

        Destroy(card.gameObject);
    }

    public void DamageHero(CardInfoScr card, bool isEnemyAttacke)
    {
        if (isEnemyAttacke)
            EnemyHP = Mathf.Clamp(EnemyHP - card.SelfCard.Attack, 0, int.MaxValue);
        else PlayerHP = Mathf.Clamp(PlayerHP - card.SelfCard.Attack, 0, int.MaxValue);

        ShowHP();
        card.DeHighLightCard();
        ChackForResult();
    }

    void ChackForResult()
    {
        if (EnemyHP == 0 || PlayerHP == 0)
        {
            ResultGO.SetActive(true);
            StopAllCoroutines();

            if (EnemyHP == 0)
                ResultText.text = "You won";
            else ResultText.text = "You lose";
        }
        else
        {

        }

        StopAllCoroutines();
    }
}
