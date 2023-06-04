using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/FrostBolt")]
public class FrostBolt : DefaultProjectileAbility
{
    public Buff chillBuff;


    public void OnEnable()
    {
        // Create Chill buff
        chillBuff = BuffFactory.CreateBuff("Chill", 6f, true, 5);
        chillBuff.statModifier = new StatsModifier
        {
            movementSpeed = -0.1f,
            attackSpeed = -0.05f
        };

        // Create Frozen buff

        abilityName = "Frost Bolt";
        abilityDescription = "Shoot a frostbolt that applies Chill and Frozen buffs";
    }

    public override void Activate(AbilityData abilityData)
    {
        base.Activate(abilityData);
    }

    public override void OnAbilityObjectHit(AbilityObject abilityObject, GameObject target)
    {
        base.OnAbilityObjectHit(abilityObject, target);
        Debug.Log("FrostBolt hit");
        if (abilityObject.data.CasterStats != null)
        {
            HealthController targetHealth = target.GetComponent<HealthController>();
            if (targetHealth != null)
            {
                float damage = abilityObject.data.damage;
                targetHealth.TakeDamage(damage, abilityObject.data.CasterStats.gameObject);

                // Apply Chill buff to the target
                BuffSystem buffSystem = target.GetComponent<BuffSystem>();
                BuffSystem casterBuffSyste = abilityObject.data.CasterStats.GetComponent<BuffSystem>();
                if (buffSystem != null)
                {
                    Debug.Log("Applying Chill buff");
                        // Apply new Chill buff
                        buffSystem.AddBuff(chillBuff, target,casterBuffSyste);
                    
                }
            }
        }
    }
  
}
