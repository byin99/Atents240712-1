using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackState : IState
{
    /// <summary>
    /// 공격 대상
    /// </summary>
    protected IBattler attackTarget = null;

    float attackCoolTime = 0.0f;

    // 상태머신에게 받는 것들
    EnemyStateMachine stateMachine;
    NavMeshAgent agent;

    public AttackState(EnemyStateMachine enemyStateMachine)
    {
        stateMachine = enemyStateMachine;
        agent = stateMachine.Agent;
    }

    public void Enter()
    {
        Debug.Log("Attack 상태 진입");
        agent.isStopped = true;         
        agent.velocity = Vector3.zero;  
        attackCoolTime = stateMachine.AttackInterval;
    }

    public void Exit()
    {
        Debug.Log("Attack 상태 나감");
    }

    public void Update()
    {
        attackCoolTime -= Time.deltaTime;

        // SearchPlayer처럼 오버랩 스피어로 처리하는 것으로 변경
        // IBattler에도 IsAlive 추가할 것

        //transform.rotation = Quaternion.Slerp(transform.rotation,
        //    Quaternion.LookRotation(attackTarget.transform.position - transform.position), 0.1f);
        //if (attackCoolTime < 0)
        //{
        //    Attack(attackTarget);
        //}
    }
}
