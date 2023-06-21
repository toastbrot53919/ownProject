using UnityEngine;
using System.Collections.Generic;
public class AbilityModifierManager
{
    private List<AbilityModifiers> modifierList = new List<AbilityModifiers>();
    private AbilityStats abilityStats;

public AbilityModifierManager()
    {
        this.abilityStats = new AbilityStats();
    }
    
    public string printAllModifers()
    {
        string s = "";
        foreach (AbilityModifiers modifier in modifierList)
        {
            s += " - "+modifier.modifierName +" - " + "\n";
            s += modifier.modifierDescription + "\n";
            s+= modifier.abilityStats.printStats();

        }
        return s;
    }
    public void AddModifier(AbilityModifiers modifiers)
    {
        modifierList.Add(modifiers);
        CalculateModifiedValue();
    }

    public void RemoveModifier(AbilityModifiers modifier)
    {
        modifierList.Remove(modifier);
        CalculateModifiedValue();
    }
    public AbilityStats GetAdditionalModifiedValue()
    {
        return abilityStats;
    }
    public AbilityStats CalculateModifiedValue()
    {
        
        abilityStats.setZero();
        foreach (AbilityModifiers modifier in modifierList)
        {
            modifier.ApplyModifier(ref abilityStats);
        }
        return abilityStats;
    }
}