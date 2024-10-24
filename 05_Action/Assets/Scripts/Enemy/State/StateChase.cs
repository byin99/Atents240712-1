using UnityEngine;

public class StateChase : IState
{
    /// <summary>
    /// 이 상태를 관리하는 상태머신
    /// </summary>
    private EnemyStateMachine stateMachine;

    // 애니메이터용 해시
    readonly int Move_Hash = Animator.StringToHash("Move");

    public StateChase(EnemyStateMachine enemyStateMachine)
    {
        stateMachine = enemyStateMachine;
    }

    public void Enter()
    {        
        Debug.Log("상태 진입 - Chase");
        stateMachine.Agent.isStopped = false;
        stateMachine.Animator.SetTrigger(Move_Hash);
    }

    public void Exit()
    {
        Debug.Log("상태 나감 - Chase");
    }

    public void Update()
    {
        if(stateMachine.SearchPlayer(out Vector3 target))   // 플레이어 찾기
        {
            stateMachine.Agent.SetDestination(target);      // 찾았으면 플레이어 위치로 이동
        }
        else
        {
            stateMachine.TransitionToWait();                // 못찾았으면 잠시 대기
        }
    }
}