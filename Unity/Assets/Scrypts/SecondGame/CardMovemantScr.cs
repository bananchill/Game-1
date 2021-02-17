using UnityEngine;
using UnityEngine.EventSystems;

public class CardMovemantScr: MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform defaultParent, defaultTempCardParent;
    public GameObject TempCardGO;
    public GameManagerScr GameManager;
    public bool IsDraggable;

    void Awake()
    {
        //TempCardGO = Resources.Load<GameObject>("TempCardGO") as GameObject;
        TempCardGO = GameObject.Find("TempCardGO");
        GameManager = FindObjectOfType<GameManagerScr>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        defaultParent = defaultTempCardParent = transform.parent;

        IsDraggable = defaultParent.GetComponent<DropPlaceScr>().type == FieldType.SELF_HAND;

        if (!IsDraggable)
            return;


        TempCardGO.transform.SetParent(defaultParent);//set patern for shadow
        //TempCardGO.transform.SetSiblingIndex(transform.GetSiblingIndex());

        transform.SetParent(defaultParent.parent);
        GetComponent<CanvasGroup>().blocksRaycasts = false;//work with other table
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!IsDraggable)
            return;

        Vector3 newPos = Camera.allCameras[0].ScreenToWorldPoint(eventData.position);
        transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);//transfer

        if (TempCardGO.transform.parent != defaultTempCardParent)
            TempCardGO.transform.SetParent(defaultTempCardParent);//for work with other table

        CheckPosition();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!IsDraggable)
            return;

        transform.SetParent(defaultParent);
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        transform.SetSiblingIndex(TempCardGO.transform.GetSiblingIndex());
        TempCardGO.transform.SetParent(GameObject.Find("Canvas").transform);
        TempCardGO.transform.localPosition = new Vector3(2340, 0);
    }

    void CheckPosition()
    {
        int newIndex = defaultTempCardParent.childCount;

        for (int i = 0; i < defaultTempCardParent.childCount; i++)
        {
            if (transform.position.x < defaultTempCardParent.GetChild(i).position.x)
            {
                newIndex = i;

                if (TempCardGO.transform.GetSiblingIndex() < newIndex)
                    newIndex--;

                break;
            }
        }
        TempCardGO.transform.SetSiblingIndex(newIndex);

    }
}
