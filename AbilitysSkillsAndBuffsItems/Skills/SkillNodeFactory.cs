using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public static class SkillNodeFactory{
    
     public static SkillNode CreateSkillNode(SkillNodeFactoryDataClass data){
        //set save path

         SkillNode skillNode = ScriptableObject.CreateInstance<SkillNode>();
         skillNode.name = data.skillName+"SkillNode";
         skillNode.skillName = data.skillName;
         skillNode.skillDescription = data.skillDescription;
         skillNode.skillPointCost = data.skillPointCost;
         skillNode.icon = data.icon;
         skillNode.mainStatRequirement = data.mainStatRequirement;
         skillNode.mainStatValue = data.mainStatValue;
         skillNode.skill = data.skill;
         skillNode.prerequisiteSkill = data.prerequisiteSkill;
         skillNode.isUnlocked = data.isUnlocked;

         AssetDatabase.CreateAsset(skillNode, "Assets/Resources/SkillNodes/"+skillNode.name+".asset");
         return skillNode;
     }

     
     

}
public class SkillNodeFactoryDataClass{
        public string skillName;
        public string skillDescription;
        public int skillPointCost;
        public Sprite icon;
        public List<Archetype> mainStatRequirement;
        public List<int> mainStatValue;
        public Skill skill;
        public SkillNode prerequisiteSkill;
        public bool isUnlocked;
        public SkillNodeFactoryDataClass(string skillName, string skillDescription, int skillPointCost, Sprite icon, List<Archetype> mainStatRequirement, List<int> mainStatValue, Skill skill, SkillNode prerequisiteSkill, bool isUnlocked){
            this.skillName = skillName;
            this.skillDescription = skillDescription;
            this.skillPointCost = skillPointCost;
            this.icon = icon;
            this.mainStatRequirement = mainStatRequirement;
            this.mainStatValue = mainStatValue;
            this.skill = skill;
            this.prerequisiteSkill = prerequisiteSkill;
            this.isUnlocked = isUnlocked;
        }
}