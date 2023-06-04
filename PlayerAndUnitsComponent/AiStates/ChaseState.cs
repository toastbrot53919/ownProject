using UnityEngine;
[CreateAssetMenu(menuName = "AI/States/ChaseState")]
public class ChaseState : AIState
{
    public float chaseSpeed = 6f;
    public float stoppingDistance = 3f;

    public override void EnterState(AIController aiController)
    {
        aiController.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = chaseSpeed;
        stoppingDistance = aiController.GetComponent<AIController>().attackRange;
    }

    public override void UpdateState(AIController aiController)
    {
        Transform target = aiController.target;

        if (target != null)
        {
            float distanceToTarget = Vector3.Distance(aiController.transform.position, target.position);

            if (distanceToTarget > stoppingDistance)
            {
                aiController.GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(target.position);
            }
            else
            {
                // Transition to another state if needed, for example, Attack
                aiController.ChangeState(aiController.attackState);
            }
        }
    }

    public override void ExitState(AIController aiController)
    {
        // Clean up or reset any variables if needed
    }
}
