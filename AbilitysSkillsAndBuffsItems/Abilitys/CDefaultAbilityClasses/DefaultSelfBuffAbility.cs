using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/DefaultSelfBuffAbility")]
public class DefaultSelfBuffAbility : DefaultAbility
{
    public Buff buff;
    public override void init()
    {
        base.init();
        buff = BuffFactory.CreateBuff("Default Self Buff", 5f, true, 3, new StatsModifier(criticalDamage: 10));

    }
    public new void Awake()
    {
        base.Awake();
        abilityName = "Default Self Buff Ability";
        abilityDescription = "This is a default self buff ability";
        BaseAbilityStats.cooldown = 10;
        BaseAbilityStats.intelligenceScaling = 1;
    }
    public override void Activate(AbilityData abilityData)
    {




        if (abilityData.casterStats == null)
        {
            Debug.LogError("CasterStats is null");
            return;
        }
        BuffSystem buffSystem = abilityData.casterStats.GetComponent<BuffSystem>();
        if (buffSystem == null)
        {
            Debug.LogError("BuffSystem is null");
            return;
        }
        buffSystem.AddBuff(buff, abilityData.casterStats.gameObject,buffSystem);
    }
    public override void OnAbilityObjectHit(AbilityObject abilityObject, GameObject target)
    {

    }
}

