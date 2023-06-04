using UnityEngine;

[CreateAssetMenu(menuName = "AI/States/IdleState")]
public class IdleState : AIState
{
    public float idleDuration = 3f;

    private float idleTime;

    public override void EnterState(AIController aiController)
    {
        idleTime = Time.time + idleDuration;
    }

    public override void UpdateState(AIController aiController)
    {
        if (Time.time > idleTime)
        {
            aiController.ChangeState(aiController.patrolState);
        }
    }

    public override void ExitState(AIController aiController)
    {
    }
}
