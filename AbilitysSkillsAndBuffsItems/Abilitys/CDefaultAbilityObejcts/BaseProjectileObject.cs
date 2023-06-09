using System.Collections;
using UnityEngine;
public class BaseProjectileObject : AbilityObject, IBouncingAbilityObject, IPiercingAbilityObject
{
    public float BounceIntensity { get; set; }
    public float BounceDuration { get; set; }

    public int bounceCount;
    public int pierceCount;


    protected override void HandleOnHit(GameObject target)
    {
    // Apply damage to the target
        if (data.casterStats != null)
        {
            HealthController targetStats = target.GetComponent<HealthController>();
            if (targetStats != null)
            {
                float damage = data.damage;
                targetStats.TakeDamage(damage, data.casterStats.gameObject);
            }
        }

        // Handle Bounce and Pierce logic
        shouldDestroy = deleteOnCollision;
        if (bounceCount > 0)
        {
            Bounce(target);
        }
        else if (pierceCount > 0)
        {
            Pierce(target);
        }

        if (shouldDestroy)
        {
            HandleOnDelete();
        }
       
    }

    public void Bounce(GameObject target)
    {
        
        shouldDestroy = false;
        bounceCount--;

        Vector3 bounceDirection = Vector3.Reflect(transform.forward, target.transform.up);
        transform.forward = bounceDirection;

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = bounceDirection * data.projectileSpeed;

        
    }

    public void Pierce(GameObject target)
    {

        pierceCount--;

        shouldDestroy = false;
    }

   
}