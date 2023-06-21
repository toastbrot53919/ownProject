using System.Collections;
using System.Collections.Generic;
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
        if (agent != null)
        {
            if (target != null)
            {
                agent.SetDestination(target.position);
                agent.stoppingDistance = stoppingDistance;
            }
            if (stunnable != null && stunnable.isStunned())
            {
                agent.isStopped = true;
            }
            else
            {
                agent.isStopped = false;
            }
        }

    }
    private Dictionary<string, Coroutine> activeDashes = new Dictionary<string, Coroutine>();

    public void StartDash(float speed, float distance, Vector3 direction, string indicatorName)
    {
        Debug.Log("StartDash");
        // Check if a dash with the same indicatorName is already active
        if (activeDashes.ContainsKey(indicatorName))
        {
            StopCoroutine(activeDashes[indicatorName]);
            activeDashes.Remove(indicatorName);
        }

        Coroutine dashCoroutine = StartCoroutine(Dash(speed, distance, direction, indicatorName));
        activeDashes.Add(indicatorName, dashCoroutine);
    }

    public void StopDash(string indicatorName)
    {
        // Check if a dash with the same indicatorName is active
        if (!activeDashes.ContainsKey(indicatorName)) return;

        StopCoroutine(activeDashes[indicatorName]);
        activeDashes.Remove(indicatorName);
    }

    IEnumerator Dash(float speed, float distance, Vector3 direction, string indicatorName)
    {
        if (stunnable.isStunned())
        {
            yield break;
        }
        var timer = 0f;
        while (timer < distance / speed)
        {
            var movementThisFrame = direction.normalized * speed * Time.deltaTime;
            GetComponent<Rigidbody>().MovePosition(transform.position + movementThisFrame);
            timer += Time.deltaTime;
            yield return null;
        }

        // Automatically remove the dash from activeDashes once it's complete
        activeDashes.Remove(indicatorName);
    }
}
