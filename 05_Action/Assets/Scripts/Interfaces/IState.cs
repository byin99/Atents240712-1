using System;

public interface IState
{
    EnemyState State { get; }

    event Action<EnemyState> onTransitionEvent;

    void Enter();
    void Exit();
    void Update();

}
