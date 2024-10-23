using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : IState
{
    EnemyStateMachine stateMachine;
    NavMeshAgent agent;
    Animator animator;

    readonly int Move_Hash = Animator.StringToHash("Move");

    public ChaseState(EnemyStateMachine enemyStateMachine)
    {
        this.stateMachine = enemyStateMachine;
        agent = stateMachine.Agent;
        animator = stateMachine.Animator;
    }

    public void Enter()
    {
        Debug.Log("Chase 상태 진입");
        agent.isStopped = false;
        animator.SetTrigger(Move_Hash);        
    }

    public void Exit()
    {
        Debug.Log("Chase 상태 나감");
    }

    public void Update()
    {
        if(stateMachine.SearchPlayer(out Vector3 target))
        {
            agent.SetDestination(target);
        }
        else
        {
            stateMachine.TransitionToWait();
        }
    }
}
