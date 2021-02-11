using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hand : MonoBehaviour, IDropHandler
{
    public List<Transform> cards;

    public void OnDrop(PointerEventData eventData)
    {
        Card card = eventData.pointerDrag.GetComponent<Card>();
        if (card)
            card.defaultParent = transform;
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
