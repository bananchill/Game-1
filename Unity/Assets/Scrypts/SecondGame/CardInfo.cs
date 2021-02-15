using UnityEngine;
using UnityEngine.UI;

class CardInfo : MonoBehaviour
{
    public Card SelfCard;
    public Image Logo;
    public Text Name;

    public void ShowCardInfo(Card card)
    {
        SelfCard = card;

        Logo.sprite = card.Logo;
        Logo.preserveAspect = true;
        Name.text = card.Name;
    }

    void Start()
    {
        ShowCardInfo(CardManagerList.allCards[transform.GetSiblingIndex()]);
    }
}
