
public class AbilityModifiers {
    public string modifierName;
    public string modifierDescription;
    public char operation;

    public AbilityStats abilityStats;

    public AbilityModifiers(string modifierName="Default", string modifierDescription="")
    {
        this.modifierName = modifierName;
        this.modifierDescription = modifierDescription;
        abilityStats = new AbilityStats();
    }

    public virtual void ApplyModifier(ref AbilityStats abilityStats){
        abilityStats.addStats(this.abilityStats);
    }

}