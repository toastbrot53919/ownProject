using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/LivingBombAbility")]
public class LivingBombAbility : DefaultAbility
{

    public LivingBombFirstExplosion firstExplosionAbility;
    public LivingBombSecondExplosion secondExplosionAbility;
    public LivingBombBuff livingBombBuff;
    public LivingBombSecondBuff livingBombSecondBuff;

    public GameObject abilityObjectFirstExplostionPrefab;
    public GameObject abilityObjectSecondExplostionPrefab;

    public new void Awake()
    {
        base.Awake();
        abilityName = "Living Bomb";
         abilityDescription = "This is a living bomb ability";
        BaseAbilityStats.cooldown = 10;
        BaseAbilityStats.intelligenceScaling = 1;

        // Create the living bomb buff
        livingBombBuff = ScriptableObject.CreateInstance<LivingBombBuff>();
        livingBombBuff.buffName = "Living Bomb Buff";
        livingBombBuff.duration = 5f;
        livingBombBuff.stackable = false;
        livingBombBuff.maxStacks = 1;

        livingBombSecondBuff = ScriptableObject.CreateInstance<LivingBombSecondBuff>();
        livingBombSecondBuff.buffName = "Living Bomb Second Buff";
        livingBombSecondBuff.duration = 5f;
        livingBombSecondBuff.stackable = false;
        livingBombSecondBuff.maxStacks = 1;

        // Create the first explosion ability
        firstExplosionAbility = CreateInstance<LivingBombFirstExplosion>();
        firstExplosionAbility.abilityObjectFirstExplostionPrefab = abilityObjectFirstExplostionPrefab;
        firstExplosionAbility.secondBombBuff = livingBombSecondBuff;


        // Create the second explosion ability
        secondExplosionAbility = CreateInstance<LivingBombSecondExplosion>();
        secondExplosionAbility.abilityObjectSecondExplostionPrefab = abilityObjectSecondExplostionPrefab;

        // Set the first explosion ability on the living bomb buff
        livingBombBuff.firstExplosionAbility = firstExplosionAbility;

        // Set the second explosion ability on the second bomb buff
        livingBombSecondBuff.secondExplosionAbility = secondExplosionAbility;

    }
    public override void Activate(AbilityData abilityData)
    {
        var targetBuffSystem = abilityData.Target.GetComponent<BuffSystem>();
        BuffSystem casterBuffSystem = abilityData.CasterStats.GetComponent<BuffSystem>();

        if (targetBuffSystem != null)
        {
            Debug.LogError("ADDED BUFF");
            targetBuffSystem.AddBuff(livingBombBuff, abilityData.Target, casterBuffSystem);
        }
    }

    public override void OnAbilityObjectHit(AbilityObject abilityObject, GameObject target)
    {
        // Override if necessary
    }
}

public class LivingBombBuff : Buff
{
    public Ability firstExplosionAbility;
    public override void InvokeOnFade(BuffInstance buffInstance, GameObject target)
    {
        base.InvokeOnFade(buffInstance, target);
        var abilityData = new AbilityData { CasterStats = buffInstance.target.GetComponent<CharacterStats>(), Target = buffInstance.target };
        firstExplosionAbility.Activate(abilityData);
        Debug.Log("FADED BUFF");
    }
    public override void InvokeOnApply(BuffInstance buffInstance, GameObject target)
    {
        base.InvokeOnApply(buffInstance, target);
        Debug.Log("APPLIED BUFF");
    }
}

[CreateAssetMenu(menuName = "Abilities/LivingBombFirstExplosion")]
public class LivingBombFirstExplosion : Ability
{
    public GameObject abilityObjectFirstExplostionPrefab;
    public Buff secondBombBuff;
    public BuffSystem casterBuffSystem;

    public override void OnAbilityObjectHit(AbilityObject abilityObject, GameObject target)
    {
        var targetBuffSystem = target.GetComponent<BuffSystem>();
        casterBuffSystem = abilityObject.data.CasterStats.GetComponent<BuffSystem>();
        if (targetBuffSystem != null)
        {
            targetBuffSystem.AddBuff(secondBombBuff, target, casterBuffSystem);
        }
    }
    public override void Activate(AbilityData abilityData)
    {

        AbilityObject abiltiyObject = GameObject.Instantiate(abilityObjectFirstExplostionPrefab, abilityData.Target.transform.position, Quaternion.identity).GetComponent<AbilityObject>();
        abiltiyObject.data = abilityData;
        abiltiyObject.ParentAbility = this;
    }

}

public class LivingBombSecondBuff : Buff
{
    public Ability secondExplosionAbility;
    public override void InvokeOnFade(BuffInstance buffInstance, GameObject target)
    {
        base.InvokeOnFade(buffInstance, target);
        var abilityData = new AbilityData { CasterStats = buffInstance.target.GetComponent<CharacterStats>(), Target = buffInstance.target };
        secondExplosionAbility.Activate(abilityData);
    }
}

public class LivingBombSecondExplosion : Ability
{
    public GameObject abilityObjectSecondExplostionPrefab;
    public override void Activate(AbilityData abilityData)
    {
        // Override if necessary
        AbilityObject abilityObject = Instantiate(abilityObjectSecondExplostionPrefab, abilityData.Target.transform.position, Quaternion.identity).GetComponent<AbilityObject>();
        abilityObject.data = abilityData;
        abilityObject.ParentAbility = this;
    }
    public override void OnAbilityObjectHit(AbilityObject abilityObject, GameObject target)
    {
        // Override if necessary
        HealthController healthController = target.GetComponent<HealthController>();
        if (healthController != null)
        {
            healthController.TakeDamage(abilityObject.data.damage, abilityObject.data.CasterStats.gameObject);
        }
    }
}
