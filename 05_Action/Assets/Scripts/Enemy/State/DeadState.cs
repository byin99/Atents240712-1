using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DeadState : IState
{
    // 상태머신에게 받는 것들
    EnemyStateMachine stateMachine;
    NavMeshAgent agent;

    public DeadState(EnemyStateMachine enemyStateMachine)
    {
        stateMachine = enemyStateMachine;
        agent = stateMachine.Agent;
    }

    public void Enter()
    {
        Debug.Log("Dead 상태 진입");
    }

    public void Exit()
    {
        Debug.Log("Dead 상태 나감");
    }

    public void Update()
    {
    }
}
