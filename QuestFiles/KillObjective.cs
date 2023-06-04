using UnityEngine;

public class KillObjective : QuestObjective
{
    [SerializeField]
    public string enemyId;
    [SerializeField]
    public int targetKills;
    [SerializeField]
    public int currentKills;

    public KillObjective(string id, string description, string enemyId, int targetKills)
    {
        this.id = id;
        this.description = description;
        this.enemyId = enemyId;
        this.targetKills = targetKills;
        this.currentKills = 0;
        this.status = ObjectiveStatus.Incomplete;
    }

    public override void UpdateProgress(string killedEnemyId)
    {
        if (killedEnemyId == "kill:"+enemyId && status != ObjectiveStatus.Completed)
        {
            currentKills++;
            Debug.LogError("Current Kills: " + currentKills);
            if (currentKills >= targetKills)
            {
                status = ObjectiveStatus.Completed;
            }
        }
    }
    public override string GetObjectiveProgress()
    {
        return currentKills + "/" + targetKills;
    }

    public override bool IsCompleted()
    {
        return status == ObjectiveStatus.Completed;
    }
}
