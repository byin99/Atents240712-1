public class StateChase : IState
{
    /// <summary>
    /// 이 상태를 관리하는 상태머신
    /// </summary>
    private EnemyStateMachine stateMachine;

    public StateChase(EnemyStateMachine enemyStateMachine)
    {
        stateMachine = enemyStateMachine;
    }

    public void Enter()
    {
    }

    public void Exit()
    {
    }

    public void Update()
    {
    }
}