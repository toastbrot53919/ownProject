using UnityEngine;
using UnityEngine.AI;

public class MovementController : MonoBehaviour
{
    IStunnable stunnable;
    public Transform target;
    public float stoppingDistance = 2f;

    private NavMeshAgent agent;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        stunnable = GetComponent<IStunnable>();
        Debug.Log("stunnable: " + stunnable);
    }

    private void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
            agent.stoppingDistance = stoppingDistance;
        }
        if(stunnable != null && stunnable.isStunned())
        {
            agent.isStopped = true;
        }
        else
        {
            agent.isStopped = false;
        }
    }
}
