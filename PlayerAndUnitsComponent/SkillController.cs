using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour,ICanStoreAndLoad<SkillControllerSaveData>
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
    public void LoadFromSaveData(SkillControllerSaveData saveData)
    {
        SkillControllerSaveData skillControllerSaveData = (SkillControllerSaveData)saveData;
        activeSkills = new List<Skill>();
        foreach (string skillName in skillControllerSaveData.activeSkills)
        {
            activeSkills.Add(SkillManager.getSkillByName(skillName));
        }
        skillTree = SkillTreeManager.getSkillTreePrefabByString(saveData.skillTree);
        availableSkillPoints = skillControllerSaveData.availableSkillPoints;

    }
    public SkillControllerSaveData GetSaveData()
    {
        return new SkillControllerSaveData(this);
    }

}
[System.Serializable]
public class SkillControllerSaveData{
    public List<string> activeSkills;
    public string skillTree;
    public int availableSkillPoints;

    public SkillControllerSaveData(SkillController skillController){
        activeSkills = new List<string>();
        foreach(Skill skill in skillController.activeSkills){
            activeSkills.Add(skill.name);
        }
        skillTree = skillController.skillTree.name;
        availableSkillPoints = skillController.availableSkillPoints;
    }
    //totalStatsModieer berechnen
}