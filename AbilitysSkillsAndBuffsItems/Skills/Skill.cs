using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : ScriptableObject
{
    public string skillName;
    public List<Archetype> archTypes;
    public StatsModifier statModifier;

    public virtual void ApplySkill(CharacterStats characterStats)
    {
        Debug.Log("Applying skill: " + skillName);
        // Implement skill-specific behavior in derived classes
    }

    public virtual void RemoveSkill(CharacterStats characterStats)
    {
        Debug.Log("Removing skill: " + skillName);
        // Implement skill-specific behavior in derived classes
    }
    public virtual void OnSpawnAbilityObject(AbilityObject abilityObject, AbilityData abilityData)
    {

    }

    
}


public enum Archetype
{
    Strength,
    Intelligence,
    Dexterity,
    Endurance,
    Wisdom
}