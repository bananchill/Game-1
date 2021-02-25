using UnityEngine;
using UnityEngine.EventSystems;

public class AttackedCard: MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        CardInfoScr card = eventData.pointerDrag.GetComponent<CardInfoScr>();

        if (card && card.SelfCard.CanAttack && transform.parent ==
            GetComponent<CardMovemantScr>().GameManager.EnemyField)
        {
            card.SelfCard.ChangeAttackState(false);

            if (card.isPlayer)
                card.DeHighLightCard();

            GetComponent<CardMovemantScr>().GameManager.CardsFight(card, GetComponent<CardInfoScr>());
        }
    }
}
