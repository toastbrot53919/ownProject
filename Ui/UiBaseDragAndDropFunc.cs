using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class UiBaseDragAndDropFunc : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public RectTransform rectTransform;
    private UIManager uiManager;
    private GameObject dragObject;
    public Vector3 originalPosition;
    private GameObject dragedObject;

    

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        uiManager = FindObjectOfType<UIManager>();
        originalPosition = rectTransform.localPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {


        dragedObject = eventData.pointerDrag;
        dragObject = new GameObject("DragObject");
        dragObject.transform.SetParent(uiManager.gameObject.transform);
        dragObject.transform.SetSiblingIndex(uiManager.gameObject.transform.childCount - 1);

        RectTransform dragRectTransform = dragObject.AddComponent<RectTransform>();
        dragRectTransform.sizeDelta = rectTransform.sizeDelta;
        dragRectTransform.position = eventData.position;



        UiAbilitySlot uiAbilitySlot = dragedObject.GetComponent<UiAbilitySlot>();
        UiItemSlot uiItemSlot = dragedObject.GetComponent<UiItemSlot>();
                Image image = dragObject.AddComponent<Image>();
        image.sprite = GetComponent<Image>().sprite;
        image.raycastTarget = false;

        if(uiAbilitySlot!=null)
        {
            image.sprite = uiAbilitySlot.icon.sprite;
        }


    }

    public void OnDrag(PointerEventData eventData)
    {
        dragObject.GetComponent<RectTransform>().position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {


        Destroy(dragObject);

        if (eventData.pointerEnter != null)
        {
            UiHotKeySlot hotkeySlot = eventData.pointerEnter.GetComponent<UiHotKeySlot>();
            UiAbilitySlot uiAbilitySlot = dragedObject.GetComponent<UiAbilitySlot>();
            UiItemSlot uiItemSlot = dragedObject.GetComponent<UiItemSlot>();


            if (hotkeySlot != null)
            {
                if(uiAbilitySlot!=null)
                {
                    hotkeySlot.ability = uiAbilitySlot.ability;
                    hotkeySlot.item = null;
                }
                else if(uiItemSlot!=null)
                {
                    hotkeySlot.item = uiItemSlot.item;
                    hotkeySlot.ability = null;
                }
                else
                {
                    hotkeySlot.item = null;
                    hotkeySlot.ability = null;
                }

                hotkeySlot.updateinfo();
            }
        }

        rectTransform.localPosition = originalPosition;
    }
}