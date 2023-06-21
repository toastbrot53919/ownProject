using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/DefaultGroupBuffAbility")]
public class DefaultGroupBuffAbility : DefaultAbility
{
    public Buff buff;
    public float radius;

   public override void init()
    {
        base.init();
        buff = BuffFactory.CreateBuff("Default Self Buff", 5f, true, 3, new StatsModifier(criticalDamage: 10));

    }

    //Needed for chaching
    private AIController aIController;
    private PlayerController playerController;
    private BuffSystem buffSystem;
    public new void Awake()
    {
        base.Awake();
        abilityName = "Default Self Buff Ability";
        abilityDescription = "This is a default self buff ability";
        BaseAbilityStats.cooldown = 10;
        BaseAbilityStats.intelligenceScaling = 1;
    }
    public override void Activate(AbilityData abilityData)
    {


        Collider[] colliders = Physics.OverlapSphere(abilityData.casterStats.transform.position, radius);
        BuffSystem casterBuffSystem = abilityData.casterStats.GetComponent<BuffSystem>();
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

