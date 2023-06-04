using UnityEngine;

[CreateAssetMenu(menuName = "AI/States/AssistState")]
public class AssistState : AIState
{
    public Transform target;
    public Ability assistAbility;
    public float assistRange = 10f;

    public override void EnterState(AIController aiController)
    {
    }

    public override void UpdateState(AIController aiController)
    {
        float distanceToTarget = Vector3.Distance(aiController.transform.position, target.position);

        if (distanceToTarget <= assistRange)
        {
            // aiController.GetComponent<AbilityController>().UseAbility(assistAbility);
        }
        else
        {
            aiController.ChangeState(aiController.followState);
        }
    }

    public override void ExitState(AIController aiController)
    {
    }
}
