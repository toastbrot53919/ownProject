using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/DefaultSelfBuffAbility")]
public class DefaultSelfBuffAbility : Ability
{
    public Buff buff;
    void OnEnable()
    {

        buff = BuffFactory.CreateBuff("Default Self Buff", 5f, true, 3, new StatsModifier(criticalDamage: 10));

    }
    public override void Activate(AbilityData abilityData)
    {


        abilityName = "Default Self Buff Ability";
        abilityDescription = "This is a default self buff ability";
        BaseAbilityStats.cooldown = 10;
        BaseAbilityStats.intelligenceScaling = 1;

        if (abilityData.CasterStats == null)
        {
            Debug.LogError("CasterStats is null");
            return;
        }
        BuffSystem buffSystem = abilityData.CasterStats.GetComponent<BuffSystem>();
        if (buffSystem == null)
        {
            Debug.LogError("BuffSystem is null");
            return;
        }
        buffSystem.AddBuff(buff, abilityData.CasterStats.gameObject,buffSystem);
    }
    public override void OnAbilityObjectHit(AbilityObject abilityObject, GameObject target)
    {

    }
}

