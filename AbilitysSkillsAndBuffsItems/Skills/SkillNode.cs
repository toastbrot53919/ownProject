using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillNode", menuName = "SkillTree/SkillNode", order = 0)]
public class SkillNode : ScriptableObject
{
    public string skillName;
    public string skillDescription;
    public int skillPointCost;
    public Sprite icon;
    public List<Archetype> mainStatRequirement;
    public List<int> mainStatValue;
    public Skill skill;
    public SkillNode prerequisiteSkill;
    public bool isUnlocked = false;


}
