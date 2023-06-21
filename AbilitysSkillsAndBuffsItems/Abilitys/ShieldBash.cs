using UnityEngine;
[CreateAssetMenu(menuName = "Abilities/ShieldBash")]
public class ShieldBash : DefaultAbility
{
    public GameObject prefabAbilityObject;

    public override void init()
    {
    
        base.init();
        abilityName = "Shield Bash";
        BaseAbilityStats.baseDamage = 50;
        BaseAbilityStats.strengthScaling = 0.5f;
        BaseAbilityStats.intelligenceScaling = 0.5f;
        animationName = "Shield Bash animation";
        stunDuration = 2f;
    }

    public override void OnAbilityObjectHit(AbilityObject abilityObject, GameObject target)
    {
        if (abilityObject.data.casterStats != null)
        {
            HealthController targetStats = target.GetComponent<HealthController>();
            if (targetStats != null)
            {
                float damage = abilityObject.data.damage;
                targetStats.TakeDamage(damage,abilityObject.data.casterStats.gameObject);

                if (abilityObject.data.stunDuration >= 0f)
                {
                    targetStats.GetComponent<IStunnable>().Stun(abilityObject.data.stunDuration);
                }
            }
        }
        RaiseOnObjectHit(abilityObject, target);
    }

    public override void Activate(AbilityData abilityData)
    {
        if (abilityData.casterStats == null) return;

        Transform casterTransform = abilityData.casterStats.transform;
        Vector3 forwardDirection = casterTransform.forward;

        GameObject abilityObjectInstance = Instantiate(prefabAbilityObject, casterTransform.position + forwardDirection, Quaternion.identity);
        AbilityObject abilityObject = abilityObjectInstance.GetComponent<AbilityObject>();
        RaiseOnObjectSpawned(abilityObject, null);

        Rigidbody rb = abilityObjectInstance.GetComponent<Rigidbody>();
        rb.velocity = forwardDirection * abilityData.projectileSpeed;

        abilityObject.data = abilityData;
        abilityData.target = null;
        abilityData.projectileSpeed = 0f;
        abilityObject.ParentAbility = this;
        abilityData.stunDuration = stunDuration;
    }
}


