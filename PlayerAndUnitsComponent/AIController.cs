using System;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public AIState currentState;
    public IdleState idleState;
    public FollowState followState;
    public AssistState assistState;
    public PatrolState patrolState;

    private NavMeshAgent navMeshAgent;

    public ChaseState chaseState;
    public AttackState attackState;

    public Transform target;
    public float aggroRadius;
    public string aggroTag;
    public float attackInterval;
    public Ability attackAbility;
    public float attackRange;
    private float DelayActionCounter;


    private AnimationController animationController;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        currentState = idleState;
        PatrolStateMonoBehaviour patrolStateMonoBehaviour = GetComponent<PatrolStateMonoBehaviour>();
        if (patrolStateMonoBehaviour != null)
        {
            patrolState.waypoints = new System.Collections.Generic.List<Transform>();
            foreach (GameObject g in patrolStateMonoBehaviour.waypoints)
            {
                patrolState.waypoints.Add(g.transform);
            }
        }
        animationController = GetComponent<AnimationController>();
    }
    public void setNextActionDelay(float delay)
    {
        DelayActionCounter = delay;
    }
    public NavMeshAgent getNavMeshAgent()
    {
        return navMeshAgent;
    }
    private void Update()   
    {
        if (navMeshAgent == null)
        {
            return;
        }
        if(DelayActionCounter > 0)
        {
            DelayActionCounter -= Time.deltaTime;
            return;
        }
        currentState.UpdateState(this);
        //if navemeshagent is moving,set animator to move
        if (navMeshAgent.velocity.magnitude > 1)
        {
            animationController.setAnimatorVariavle("Speed",1f);

        }
        else
        {
            animationController.setAnimatorVariavle("Speed", 0f);
        }

    }

    public void ChangeState(AIState newState)
    {
        Debug.Log("Change State From " + currentState + " to " + newState + "");
        currentState.ExitState(this);
        currentState = newState;
        newState.EnterState(this);
    }
    public void checkForAggro()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, aggroRadius);
        foreach (Collider collider in colliders)
        {
            if (!string.IsNullOrEmpty(collider.tag) && collider.CompareTag(aggroTag))
            {
                target = collider.gameObject.transform;
                ChangeState(chaseState);
                break;
            }
        }

    }
    float nextAttackTime = 0;
    public void attack()
    {

        if (target != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.position);
            
            if (distanceToTarget <= attackRange)
            {
                navMeshAgent.isStopped = true;
                GetComponent<Animator>().SetFloat("Speed", 0);
                // Face target
                Vector3 direction = (target.position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);


                // Use attack ability
                setNextActionDelay(animationController.returnAnimationLockTiming(attackAbility.animationName));
                GetComponent<CharacterCombatController>().PerformAbility(attackAbility, target.gameObject);


            }
            else
            {
                // Transition to another state if needed, for example, Chase
                ChangeState(chaseState);
            }
        }
    }

    internal void SetAIController(AIController aiController)
    {
        currentState = aiController.currentState;
        idleState = aiController.idleState;
        followState = aiController.followState;
        assistState = aiController.assistState;
        patrolState = aiController.patrolState;
        chaseState = aiController.chaseState;
        attackState = aiController.attackState;
        target = aiController.target;
        aggroRadius = aiController.aggroRadius;
        aggroTag = aiController.aggroTag;
        attackInterval = aiController.attackInterval;
        attackAbility = aiController.attackAbility;
        attackRange = aiController.attackRange;


    }
}

public abstract class AIState : ScriptableObject
{
    public abstract void EnterState(AIController aiController);
    public abstract void UpdateState(AIController aiController);
    public abstract void ExitState(AIController aiController);
}

