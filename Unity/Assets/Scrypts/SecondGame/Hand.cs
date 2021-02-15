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

public class Hand : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public List<Transform> cards;
    public FieldType type;

    public void OnDrop(PointerEventData eventData)
    {
        if (type != FieldType.SELF_TABLE)
            return;

        CardMovemant card = eventData.pointerDrag.GetComponent<CardMovemant>();
        if (card)
            card.defaultParent = transform;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null || type == FieldType.ENEMY_TABLE)
            return;

        CardMovemant card = eventData.pointerDrag.GetComponent<CardMovemant>();

        if (card)
            card.defaultTempCardParent = transform;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;

        CardMovemant card = eventData.pointerDrag.GetComponent<CardMovemant>();

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
