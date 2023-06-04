using UnityEditor;
using UnityEngine;

public abstract class QuestObjective
{
    public string id;
    public string description;
    public ObjectiveStatus status;
    public abstract bool IsCompleted();

    public abstract void UpdateProgress(string infoId);
    public abstract string GetObjectiveProgress();

}
public enum ObjectiveStatus { Completed,Incomplete };

public class GatherObjective : QuestObjective
{
    public string itemId;
    public int targetItems;
    public int currentItems;

    public GatherObjective(string id, string description, string itemId, int targetItems)
    {
        this.id = id;
        this.description = description;
        this.itemId = itemId;
        this.targetItems = targetItems;
        this.currentItems = 0;
        this.status = ObjectiveStatus.Incomplete;
    }

    public override void UpdateProgress(string gatheredItemId)
    {
        if (gatheredItemId == "gather:"+itemId && status != ObjectiveStatus.Completed)
        {
            currentItems++;
            if (currentItems >= targetItems)
            {
                status = ObjectiveStatus.Completed;
            }
        }
    }

    public override bool IsCompleted()
    {
        return status == ObjectiveStatus.Completed;
    }
    public override string GetObjectiveProgress()
    {
        return currentItems + "/" + targetItems;
    }
}

public class InspectObjective : QuestObjective
{
    public string locationId;
    public bool locationInspected;

    public InspectObjective(string id, string description, string locationId)
    {
        this.id = id;
        this.description = description;
        this.locationId = locationId;
        this.locationInspected = false;
        this.status = ObjectiveStatus.Incomplete;
    }

    public override void UpdateProgress(string inspectedLocationId)
    {
        if (inspectedLocationId == "visit:"+locationId && !locationInspected)
        {
            locationInspected = true;
            status = ObjectiveStatus.Completed;
        }
    }

    public override bool IsCompleted()
    {
        return locationInspected;
    }
    public override string GetObjectiveProgress()
    {
        return locationInspected ? "Inspected" : "Not Inspected";
    }
}

public class ActivateObjective : QuestObjective
{
    public string altarId;
    public bool altarActivated;

    public ActivateObjective(string id, string description, string altarId)
    {
        this.id = id;
        this.description = description;
        this.altarId = altarId;
        this.altarActivated = false;
        this.status = ObjectiveStatus.Incomplete;
    }

    public override void UpdateProgress(string activatedAltarId)
    {
        if (activatedAltarId == "activate:"+altarId && !altarActivated)
        {
            altarActivated = true;
            status = ObjectiveStatus.Completed;
        }
    }

    public override bool IsCompleted()
    {
        return altarActivated;
    }
    public override string GetObjectiveProgress()
    {
        return altarActivated ? "Activated" : "Not Activated";
    }
}
