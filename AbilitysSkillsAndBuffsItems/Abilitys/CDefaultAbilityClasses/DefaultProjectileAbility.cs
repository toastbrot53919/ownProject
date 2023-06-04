using System.Collections;
using UnityEngine;

// Base Projectile Ability class
[CreateAssetMenu(menuName = "Abilities/DefaultProjectileAbility")]
public class DefaultProjectileAbility : Ability
{
    public GameObject projectilePrefab;

    public override void OnAbilityObjectHit(AbilityObject abilityObject, GameObject target)
    {
        if(abilityObject.data.CasterStats != null)
        {
            HealthController targetStats = target.GetComponent<HealthController>();
            if (targetStats != null)
            {
                float damage = abilityObject.data.damage;
                targetStats.TakeDamage(damage,abilityObject.data.CasterStats.gameObject);
            }
        }
         RaiseOnObjectHit(abilityObject,target);
  

    }

    public override void Activate(AbilityData abilityData)
    {
        if (abilityData.CasterStats == null) return;

        Transform firePoint = abilityData.CasterStats.GetComponent<AbilityController>().firePoint;


        GameObject projectileInstance = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        BaseProjectileObject abilityObject = projectileInstance.GetComponent<BaseProjectileObject>();
        RaiseOnObjectSpawned(abilityObject,null);
        PlayerController playerController = abilityData.CasterStats.GetComponent<PlayerController>();
        Rigidbody rb = projectileInstance.GetComponent<Rigidbody>();
        if(playerController != null)
        {
            projectileInstance.transform.rotation = playerController.GetCamera().transform.rotation;
        }
        else if (abilityData.Target != null)
        {
            projectileInstance.transform.LookAt(abilityData.Target.transform);
        }
   


        rb.velocity = projectileInstance.transform.forward * abilityData.projectileSpeed;

        
        abilityObject.ParentAbility = this;
        abilityObject.data = abilityData;

    }
}

// Base Projectile Object class

