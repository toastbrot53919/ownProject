using UnityEngine;

public static class AbilityFactory
{
public static Ability CreateAbility(string abilityName = "", string abilityDescription = "", Sprite icon = null, AbilityStats baseAbilityStats = null)
{
    Ability ability = ScriptableObject.CreateInstance<Ability>();
    ability.abilityName = abilityName;
    ability.abilityDescription = abilityDescription;
    ability.icon = icon;
    ability.BaseAbilityStats = baseAbilityStats ?? new AbilityStats(); // Use default ability stats if not provided
    AbilityModifierManager abilityModifierManager = new AbilityModifierManager();
    ability.init();
    ability.updateAbilityStats();

    return ability;
}

}
