using UnityEngine;
[CreateAssetMenu(menuName = "Abilities/Fireball")]
class FireBall : DefaultProjectileAbility {
    //Expecting BaseProjectileObject to be a prefab
  public override void OnAbilityObjectHit(AbilityObject abilityObject, GameObject target) {
    if (abilityObject.data.CasterStats != null) {
      HealthController targetHealth = target.GetComponent<HealthController>();
      if (targetHealth != null) {
        float damage = abilityObject.data.damage;
        targetHealth.TakeDamage(damage,abilityObject.data.CasterStats.gameObject);
      }
    }
    RaiseOnObjectHit(abilityObject, target);
  }

    public override void Activate(AbilityData abilityData) {
    if (abilityData.CasterStats == null) return;

    Transform firePoint = abilityData.CasterStats.GetComponent<AbilityController>().firePoint;

    GameObject projectileInstance = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
    BaseProjectileObject abilityObject = projectileInstance.GetComponent<BaseProjectileObject>();
    RaiseOnObjectSpawned(abilityObject, null);

    Rigidbody rb = projectileInstance.GetComponent<Rigidbody>();
    rb.velocity = firePoint.forward * abilityData.projectileSpeed;

    abilityObject.ParentAbility = this;
    abilityObject.data = abilityData;
  }
}
