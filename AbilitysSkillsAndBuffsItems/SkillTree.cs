using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillTree", menuName = "SkillTree/SkillTree", order = 1)]
public class SkillTree : ScriptableObject
{
    public List<SkillNode> skillNodes;
    public SkillTree()
    {
        skillNodes = new List<SkillNode>();
    }

    public void AddSkillNode(SkillNode skillNode)
    {
        skillNodes.Add(skillNode);
    }
    internal bool IsVisible(SkillNode skillNode)
    {
        return true;
    }
    private void Awake()
    {
        resetAllNodes();
    }
    public void resetAllNodes()
    {
        foreach (SkillNode node in skillNodes)
        {
            node.isUnlocked = false;

        }
    }
}

