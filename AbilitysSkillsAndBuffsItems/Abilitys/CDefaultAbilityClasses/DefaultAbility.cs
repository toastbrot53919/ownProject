using UnityEngine;

public class DefaultAbility : Ability
{
    public void Awake()
    {
        abilityName = "Default";
        abilityDescription = "Default";

    }
    public void OnEnable(){
        if(BaseAbilityStats == null){
            BaseAbilityStats = new AbilityStats();
        }
        if(TotalAbilityStats == null){
            TotalAbilityStats = new AbilityStats();
        }
        if(abilityModifierManager == null){
            abilityModifierManager = CreateInstance<AbilityModifierManager>();
        }
        updateAbilityStats();
    }
    public override void Activate(AbilityData abilityData)
    {
         Debug.Log("Default Ability");
    }

    public override void OnAbilityObjectHit(AbilityObject abilityObject, GameObject target)
    {
    }
}