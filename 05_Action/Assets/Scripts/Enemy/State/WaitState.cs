using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaitState : IState
{
    float waitCounter;

    // 상태머신에게 받는 것들
    float waitTime;
    EnemyStateMachine stateMachine;
    NavMeshAgent agent;
    Animator animator;

    readonly int Stop_Hash = Animator.StringToHash("Stop");

    public EnemyState State => EnemyState.Wait;

    public event Action<EnemyState> onTransitionEvent;

    public WaitState(EnemyStateMachine enemyStateMachine)
    {
        stateMachine = enemyStateMachine;
        agent = stateMachine.Agent;
        animator = stateMachine.Animator;
        waitTime = stateMachine.WaitTime;            
    }

    public void Enter()
    {
        Debug.Log("Wait 상태 진입");
        agent.isStopped = true;         // agent 정지
        agent.velocity = Vector3.zero;  // agent에 남아있던 운동량 제거
        animator.SetTrigger(Stop_Hash); // 애니메이션 정지
        waitCounter = waitTime;
    }

    public void Exit()
    {
    }

    public void Update()
    {
        waitCounter -= Time.deltaTime;
        if (waitCounter < 0)
        {
            onTransitionEvent?.Invoke(EnemyState.Patrol);
        }
    }
}
