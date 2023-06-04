using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestLogUIController : MonoBehaviour
{
    public TMP_Text questLogText;
    public TMP_Text trackingText;

    private QuestSystem questSystem;
    private List<Quest> trackingQuests = new List<Quest>();

    private void Awake()
    {
        questSystem = FindObjectOfType<QuestSystem>();
        if (questSystem == null)
        {
            Debug.LogError("No QuestSystem found in the scene!");
        }
    }

    private void Start()
    {
        UpdateQuestLog();
    }

    public void UpdateQuestLog()
    {
        string questLogString = "";
        foreach (Quest quest in questSystem.quests)
        {
            questLogString += $"[{quest.status}] {quest.title}\n";
            foreach (QuestObjective objective in quest.objectives)
            {
                questLogString += $"- {objective.description} ({objective.GetObjectiveProgress()})\n";
            }
            questLogString += "\n";
        }
        questLogText.text = questLogString;

        string trackingString = "Tracking: ";
        foreach (Quest quest in trackingQuests)
        {
            trackingString += quest.title + ", ";
        }
        trackingText.text = trackingString.TrimEnd(',', ' ');
    }

    public void AddQuestToTrack(int questID)
    {
        Quest quest = questSystem.GetQuestByID(questID);
        if (quest != null && !trackingQuests.Contains(quest))
        {
            trackingQuests.Add(quest);
            UpdateQuestLog();
        }
    }

    public void RemoveQuestToTrack(int questID)
    {
        Quest quest = questSystem.GetQuestByID(questID);
        if (quest != null && trackingQuests.Contains(quest))
        {
            trackingQuests.Remove(quest);
            UpdateQuestLog();
        }
    }
}
