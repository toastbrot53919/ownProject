using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestBookUIController : MonoBehaviour
{
    public TMP_Text titleText;
    public TMP_Text descriptionText;
    public TMP_Text objectivesText;
    public ScrollRect questListScrollRect;
    public GameObject questListItemPrefab;
    public Transform questListContent;
    
    public QuestSystem questSystem;
  

    private void Awake()
    {
       
    }

    private void Start()
    {  
        UpdateQuestList();
    }

    public void UpdateQuestList()
    {
        // Clear the quest list content
        foreach (Transform child in questListContent)
        {
            Destroy(child.gameObject);
        }

        // Re-populate the quest list content
        foreach (Quest quest in questSystem.quests)
        {
            GameObject questListItem = Instantiate(questListItemPrefab, questListContent);
            questListItem.gameObject.SetActive(true);
            questListItem.GetComponentInChildren<TMP_Text>().text = quest.title;
            questListItem.GetComponent<Button>().onClick.AddListener(() => ShowQuestInformation(quest));
        }

        // Reset the scroll position of the quest list
        questListScrollRect.verticalNormalizedPosition = 1f;
    }

    public void ShowQuestInformation(Quest quest)
    {
        // Set the quest information text fields to the current quest's data
        titleText.text = quest.title;
        descriptionText.text = quest.description;
        string objectivesString = "";
        foreach(QuestObjective objective in quest.objectives)
        {
            objectivesString += $"-({objective.GetObjectiveProgress()})\n";
        }   
        
    }
}
