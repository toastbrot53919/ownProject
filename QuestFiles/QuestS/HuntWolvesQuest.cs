using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "HuntWolves", menuName = "ScriptableObjects/Quests/HuntWolves", order = 1)]
public class HuntWolvesQuest : Quest
{
    public HuntWolvesQuest() : base(1, "Hunt the Wolves", "The village has been suffering from frequent wolf attacks. They've asked you to hunt down 10 wolves and bring back their pelts as proof.")
    {
        // Add a KillObjective to the list of objectives
        AddObjective(new KillObjective("HuntWolvesObjective", "Hunt 10 Wolves", "Wolf", 10));
    }
}
