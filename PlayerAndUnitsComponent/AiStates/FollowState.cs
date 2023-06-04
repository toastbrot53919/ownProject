using UnityEngine;

[CreateAssetMenu(menuName = "AI/States/FollowState")]
public class FollowState : AIState
{
    public Transform target;
    public float stoppingDistance = 2f;

    public override void EnterState(AIController aiController)
    {
    }

    public override void UpdateState(AIController aiController)
    {
        float distanceToTarget = Vector3.Distance(aiController.transform.position, target.position);

        if (distanceToTarget > stoppingDistance)
        {
            aiController.GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(target.position);
        }
        else
        {
            aiController.GetComponent<UnityEngine.AI.NavMeshAgent>().ResetPath();
        }
    }

    public override void ExitState(AIController aiController)
    {
    }
}
