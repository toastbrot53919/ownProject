using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "AI/States/PatrolState")]
public class PatrolState : AIState
{
    public List<Transform> waypoints;
    public float patrolSpeed = 3f;
    public float waitTime = 3f;

    private int currentWaypoint;
    private float waitEndTime;

    public override void EnterState(AIController aiController)
    {
        aiController.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = patrolSpeed;
        currentWaypoint = 0;
    }

    public override void UpdateState(AIController aiController)
    {   
        aiController.checkForAggro();

        UnityEngine.AI.NavMeshAgent agent = aiController.GetComponent<UnityEngine.AI.NavMeshAgent>();

        if (waypoints.Count > 0)
        {
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                if (Time.time > waitEndTime)
                {
                    currentWaypoint = (currentWaypoint + 1) % waypoints.Count;
                    agent.SetDestination(waypoints[currentWaypoint].position);
                    waitEndTime = Time.time + waitTime;
                }
            }
        }
    }

    public override void ExitState(AIController aiController)
    {
        // Clean up or reset any variables if needed
    }
}