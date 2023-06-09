﻿using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/SimpleStrike")]
public class SimpleStrike : DefaultAbility
{
    // SimpleStrike specific properties, if any
    public GameObject MeelePrefab;
    public float lifeTime = 0.5f;

    public override void OnAbilityObjectHit(AbilityObject abilityObject, GameObject target)
    {
        HealthController healthController = target.GetComponent<HealthController>();
        if (healthController != null)
        {

            healthController.TakeDamage(abilityObject.data.damage,abilityObject.data.casterStats.gameObject);
        }
    }


    public override void Activate(AbilityData abilityData)
    {
        GameObject meleeStrikeInstance = Instantiate(MeelePrefab, abilityData.casterController.firePoint.position, Quaternion.identity);
        AbilityObject abilityObject = meleeStrikeInstance.GetComponent<AbilityObject>();

        abilityObject.ParentAbility = this;
        abilityObject.data = abilityData;
        Destroy(meleeStrikeInstance, lifeTime);
    }


}


