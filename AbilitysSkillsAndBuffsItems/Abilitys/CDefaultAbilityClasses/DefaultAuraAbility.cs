// DefaultAura.cs
using UnityEngine;
using System.Collections;
[CreateAssetMenu(menuName = "Abilities/DefaultAuraAbility")]
public class DefaultAuraAbility : DefaultAbility 
{
    public GameObject auraPrefab;
    public float damageInterval = 0.5f;
    private float lastDamageTime;
    AbilityObject abilityObject;

    

    
    private new void OnEnable(){
        BaseAbilityStats = new AbilityStats();
        TotalAbilityStats = new AbilityStats();
        BaseAbilityStats.cooldown = 2;
        updateAbilityStats();
        animingMode = AnimingMode.PrePositionPlacement;

    }
    public override void Activate(AbilityData abilityData)
    {
        Debug.Log("Activate");
        useUpdate = true;

         abilityObject = Instantiate(auraPrefab, abilityData.Target.transform.position, Quaternion.identity).GetComponent<AbilityObject>();
        abilityObject.data = abilityData;
        abilityObject.ParentAbility = this;
        abilityObject.OnDelete+=DeactivateUpdate;

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
                healthController.TakeDamage(abilityObject.data.damage, abilityObject.data.CasterStats.gameObject);
            }

        
    }
    public override void OnUpdate()
    {
       // abilityObject.transform.position = abilityObject.data.CasterStats.transform.position;
    }
    public  void DeactivateUpdate(){
        useUpdate = false;
    }
    public override void Deactivate()
    {
        if(abilityObject!=null){
          //  abilityObject.Delete();
        }

        
    }
}
