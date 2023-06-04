using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonWithToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public SkillNode skillNode;
    private PlayerController playerController;
    public UIManager uiManager;
    private GameObject toolTipObject;

    private Button button;

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        uiManager = FindObjectOfType<UIManager>();
        toolTipObject = uiManager.tooltip;
        toolTipObject.SetActive(false);

        button = GetComponent<Button>();
        button.onClick.AddListener(TryLearn);
        if(skillNode!=null){
            GetComponent<Image>().sprite = skillNode.icon;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowToolTip();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HideToolTip();
    }

    private void ShowToolTip()
    {
        uiManager.OpenToolTip(skillNode, gameObject.GetComponent<RectTransform>().position);
    }

    private void HideToolTip()
    {
        uiManager.CloseToolTip();
    }

    private void TryLearn()
    {
        playerController.TryUnlockSkillNode(skillNode);
    }
}
