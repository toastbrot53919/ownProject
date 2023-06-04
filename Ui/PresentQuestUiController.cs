using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PresentQuestUiController : MonoBehaviour
{
    [SerializeField] private TMP_Text questTitle;
    [SerializeField] private TMP_Text questDescription;
    [SerializeField] private Button acceptButton;
    [SerializeField] private Button declineButton;

    private QuestSystem questSystem;
    void Start()
    {
        questSystem = FindObjectOfType<QuestSystem>();
    }
  

    public void showQuestInfo(Quest quest,UIManager UIManager)
    {
        questTitle.text = quest.title;
        questDescription.text = quest.description;
        acceptButton.onClick.AddListener(() => questSystem.AddQuest(quest));
        acceptButton.onClick.AddListener(() => UIManager.hideQuestUiPresenter());
        declineButton.onClick.AddListener(() => UIManager.hideQuestUiPresenter());
        
    }

}
