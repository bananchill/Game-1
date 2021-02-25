using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum FieldType
{
    SELF_HAND,
    SELF_TABLE,
    ENEMY_HAND,
    ENEMY_TABLE
}

public class DropPlaceScr : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public List<Transform> cards;
    public FieldType type;

    public void OnDrop(PointerEventData eventData)
    {
        if (type != FieldType.SELF_TABLE)
            return;

        CardMovemantScr card = eventData.pointerDrag.GetComponent<CardMovemantScr>();
        if (card && card.GameManager.PlayerFieldCards.Count <= 6 &&
            card.GameManager.IsPlayerTurn)
        {
            card.GameManager.PlayerFieldCards.Remove(card.GetComponent<CardInfoScr>());
            card.GameManager.PlayerFieldCards.Add(card.GetComponent<CardInfoScr>());
            card.defaultParent = transform;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null || type == FieldType.ENEMY_TABLE || type == FieldType.ENEMY_HAND
            || type == FieldType.SELF_HAND)
            return;

        CardMovemantScr card = eventData.pointerDrag.GetComponent<CardMovemantScr>();

        if (card)
            card.defaultTempCardParent = transform;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;

        CardMovemantScr card = eventData.pointerDrag.GetComponent<CardMovemantScr>();

        if (card && card.defaultTempCardParent == transform)
            card.defaultTempCardParent = card.defaultParent;
    }

    void Start()
    {
        cards = new List<Transform>();
        foreach (Transform obj in transform.GetComponentsInChildren<Transform>())
        {
            if (obj.CompareTag("Card"))
                cards.Add(obj);
        }
    }
}
