using UnityEngine;
[CreateAssetMenu(fileName = "FireBallMastery", menuName = "Skill/FireBallMastery", order = 1)]
public class FireballMasterySkill : Skill
{
    [SerializeField]
    private GameObject explosionPrefab;

    public override void ApplySkill(CharacterStats playerStats)
    {
        Debug.Log("Apply Skill");
        FireBall fireballAbility = GetFireballAbility(playerStats);
        if (fireballAbility != null)
        {
            Debug.Log("Fireball Ability found");
            fireballAbility.OnAbilityObjectHitEvent += ExplodeOnHit;
        }
    }

    public override void RemoveSkill(CharacterStats playerStats)
    {
        FireBall fireballAbility = GetFireballAbility(playerStats);
        if (fireballAbility != null)
        {
            fireballAbility.OnAbilityObjectHitEvent -= ExplodeOnHit;
        }
    }

    private FireBall GetFireballAbility(CharacterStats playerStats)
    {
        AbilityController abilityController = playerStats.GetComponent<AbilityController>();
        return abilityController.learnedAbilitys.Find(a => a is FireBall) as FireBall;
    }

    private void ExplodeOnHit(AbilityObject abilityObject, GameObject target)
    {
        Debug.Log("EXPLODE ON Hit");
        ApplyDamageToTargets(abilityObject.transform.position, 2f, abilityObject.data.damage * 0.5f);
        InstantiateExplosion(abilityObject.transform.position);
    }

    private void ApplyDamageToTargets(Vector3 position, float radius, float damage)
    {
   
    }

    private void InstantiateExplosion(Vector3 position)
    {
        if (explosionPrefab != null)
        {
            GameObject explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
            // Add additional logic for the explosion, such as configuring the explosion's lifetime or assigning its parent
        }
        else
        {
            Debug.LogWarning("No explosion prefab assigned to FireballMasterySkill.");
        }
    }
}
