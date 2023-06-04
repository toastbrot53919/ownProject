using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Quest", menuName = "ScriptableObjects/Quest", order = 1)]
public class Quest : ScriptableObject
{
    public int id;
    public string title;
    public string description;
    public List<QuestObjective> objectives;
    public List<Reward> rewards;
    public QuestStatus status;

    public Quest(int id, string title, string description)
    {
        this.id = id;
        this.title = title;
        this.description = description;
        this.objectives = new List<QuestObjective>();
        this.rewards = new List<Reward>();
        this.status = QuestStatus.NotStarted;
    }

    public void AddObjective(QuestObjective objective)
    {
        objectives.Add(objective);
    }

    public void AddReward(Reward reward)
    {
        rewards.Add(reward);
    }

    // The missing CheckAndUpdateObjectives method
    public void CheckAndUpdateObjectives(string objectiveId)
    {
        foreach (QuestObjective objective in objectives)
        {
            if ( objective.status == ObjectiveStatus.Incomplete)
            {
                objective.UpdateProgress(objectiveId);
                if (objective.status == ObjectiveStatus.Completed)
                {
                    CheckQuestCompletion();
                }
                break;
            }
        }
    }

    private void CheckQuestCompletion()
    {
        bool allObjectivesComplete = true;
        foreach (QuestObjective objective in objectives)
        {
            if (objective.status != ObjectiveStatus.Completed)
            {
                allObjectivesComplete = false;
                break;
            }
        }

        if (allObjectivesComplete)
        {
            status = QuestStatus.Completed;
        }
    }
}
public enum QuestStatus { NotStarted, InProgress, Completed }