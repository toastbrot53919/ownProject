// DefaultAura.cs
using UnityEngine;
using System.Collections;
[CreateAssetMenu(menuName = "Abilities/DefaultAOEAbility")]
public class DefaultAOEAbility : DefaultAbility 
{
    public GameObject auraPrefab;
    public float damageInterval = 0.5f;
    private float lastDamageTime;
    AbilityObject abilityObject;

    

    public new void Awake()
    {
        base.Awake();
        abilityName = "Default Aura Ability";
        abilityDescription = "This is a default aura ability";
        BaseAbilityStats.cooldown = 2;
        BaseAbilityStats.intelligenceScaling = 1;
        animingMode = AnimingMode.PrePositionPlacement;
    }
    public new void init(){
        base.init();
  

    }
    public override void Activate(AbilityData abilityData)
    {
        Debug.Log("Activate");
        useUpdate = true;

         abilityObject = Instantiate(auraPrefab, abilityData.target.transform.position, Quaternion.identity).GetComponent<AbilityObject>();

        abilityObject.data = abilityData;
        abilityObject.ParentAbility = this;

    }
    float timestart=0;
    public override void OnAbilityObjectHit(AbilityObject abilityObject, GameObject target)
    {
            if(timestart==0){
                timestart=Time.time;
            }

            HealthController healthController = target.GetComponent<HealthController>();
            if (healthController != null) 
            {
                healthController.TakeDamage(abilityObject.data.damage, abilityObject.data.casterStats.gameObject);
            }

        
    }
}
