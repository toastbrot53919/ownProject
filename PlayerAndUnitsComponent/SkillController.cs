using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    public List<Skill> activeSkills;
    public SkillTree skillTree;
    public int availableSkillPoints;
    public StatsModifier totalStatsModier;
    public delegate void SkillEvent(SkillNode skillNode);
    public event SkillEvent OnSkillUnlocked;
    public event SkillEvent OnSkillUnlearnd;




    public void LearnSkill(SkillNode skillNode)
    {

        // Call event to update the UI, etc.

        activeSkills.Add(skillNode.skill);
        skillNode.skill.ApplySkill(this.gameObject.GetComponent<CharacterStats>());
        totalStatsModier.Add(skillNode.skill.statModifier);
        availableSkillPoints -= skillNode.skillPointCost;
        OnSkillUnlocked?.Invoke(skillNode);

    }
    public void UnlearnSkill(SkillNode skillNode)
    {
        if (activeSkills.Remove(skillNode.skill))
        {
            totalStatsModier.Sub(skillNode.skill.statModifier);
        }
        availableSkillPoints += skillNode.skillPointCost;
        skillNode.skill.RemoveSkill(this.gameObject.GetComponent<CharacterStats>());
        invokeOnSkillUnlearnd(skillNode);
    }
    public void invokeOnSkillUnlocked(SkillNode skillNode)
    {
        OnSkillUnlocked?.Invoke(skillNode);
    }
    public void invokeOnSkillUnlearnd(SkillNode skillNode)
    {
        OnSkillUnlearnd?.Invoke(skillNode);
    }

}

