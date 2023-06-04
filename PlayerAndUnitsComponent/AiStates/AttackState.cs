using UnityEngine;
[CreateAssetMenu(menuName = "AI/States/AttackState")]
public class AttackState : AIState
{
    public Transform target;
    public Ability attackAbility;
    public float attackRange = 5f;
    public float attackInterval = 1f;

    private float nextAttackTime;

    public override void EnterState(AIController aiController)
    {
        nextAttackTime = Time.time;
    }

    public override void UpdateState(AIController aiController)
    {
        aiController.attack();
    }

    public override void ExitState(AIController aiController)
    {
        // Clean up or reset any variables if needed
    }
}
