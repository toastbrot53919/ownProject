using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UiHotKeySlot : MonoBehaviour
{
    public Image icon;
    public int hotkeyIndex;
    private UIManager uiManager;
    private RectTransform rectTransform;
    public Vector3 originalPosition;
    public Ability ability;
    public Item item;

    private HotkeyController hotkeyController;

    private void Start()
    {
        hotkeyController = FindObjectOfType<HotkeyController>();
        uiManager = FindObjectOfType<UIManager>();
        rectTransform = GetComponent<RectTransform>();
        icon = GetComponent<Image>();
        originalPosition = rectTransform.localPosition;

        
        
    }
    public void updateinfo(){
        if(ability!=null){
            icon.sprite = ability.icon;
            hotkeyController.hotkeys[hotkeyIndex].ability = ability;
            hotkeyController.hotkeys[hotkeyIndex].item = null;
        }
        if(item!=null){
            icon.sprite = item.icon;
            hotkeyController.hotkeys[hotkeyIndex].item = item;
            hotkeyController.hotkeys[hotkeyIndex].ability = null;
        }


    }
    public  void OnPointerEnter(PointerEventData eventData)
    {
       uiManager.OpenToolTip(hotkeyController.hotkeys[hotkeyIndex], rectTransform.position);
    }

    public  void OnPointerExit(PointerEventData eventData)
    {
        uiManager.CloseToolTip();
    }


}
