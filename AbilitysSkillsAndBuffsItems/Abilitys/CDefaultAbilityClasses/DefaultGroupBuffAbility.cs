using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/DefaultGroupBuffAbility")]
public class DefaultGroupBuffAbility : Ability
{
    public Buff buff;
    public float radius;

   void OnEnable()
    {

        buff = BuffFactory.CreateBuff("Default Self Buff", 5f, true, 3, new StatsModifier(criticalDamage: 10));

    }

    //Needed for chaching
    private AIController aIController;
    private PlayerController playerController;
    private BuffSystem buffSystem;
    public override void Activate(AbilityData abilityData)
    {

        abilityName = "DefaultGroupBuffAbility";
        abilityDescription = "This is a DefaultGroupBuffAbility";
        BaseAbilityStats.cooldown = 10;
        BaseAbilityStats.intelligenceScaling = 1;

        Collider[] colliders = Physics.OverlapSphere(abilityData.CasterStats.transform.position, radius);
        BuffSystem casterBuffSystem = abilityData.CasterStats.GetComponent<BuffSystem>();
        foreach (Collider collider in colliders)
        {
            buffSystem = collider.gameObject.GetComponent<BuffSystem>();

            if (buffSystem == null)
            {
                continue;
            }
            aIController = collider.gameObject.GetComponent<AIController>();
            playerController = collider.gameObject.GetComponent<PlayerController>();


            if (aIController != null)
            {
                if (aIController.aggroTag == "Player")
                {
                    continue;
                }
                buffSystem.AddBuff(buff, collider.gameObject,casterBuffSystem);
                continue;

            }
            else if (playerController != null)
            {
                buffSystem.AddBuff(buff, collider.gameObject,casterBuffSystem);
                continue;
            }
        }
    }

    public override void OnAbilityObjectHit(AbilityObject abilityObject, GameObject target)
    {

    }
}

