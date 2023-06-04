using UnityEngine;
using System.Collections.Generic;

public class QuestSystem : MonoBehaviour
{
    public List<Quest> quests;
    public UIManager uiManager;

 
public Quest GetQuestByID(int questID)
{
    foreach (Quest quest in quests)
    {
        if (quest.id == questID)
        {
            return quest;
        }
    }
    return null;
}
    private void Start()
    {
        quests = new List<Quest>();
    }

    public void AddQuest(Quest quest)
    {
        uiManager.updateQuestBook();
        quests.Add(quest);
    }

    public void RemoveQuest(int questId)
    {

        Quest questToRemove = quests.Find(q => q.id == questId);
        if (questToRemove != null)
        {
          
            quests.Remove(questToRemove);
        }
    }
        public void UpdateQuestObjective(string objectiveId)
    {
        uiManager.updateQuestBook();
        foreach (Quest quest in quests)
        {
            if (quest.status != QuestStatus.Completed)
            {
                quest.CheckAndUpdateObjectives(objectiveId);
            }
        }
    }

   
}
