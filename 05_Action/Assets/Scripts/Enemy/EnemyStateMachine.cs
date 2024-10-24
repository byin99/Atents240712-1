using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    // 대기 상태용 변수 및 프로퍼티들 --------------------------------------------------------------

    /// <summary>
    /// 대기 상태로 들어갔을 때 기다리는 시간
    /// </summary>
    [SerializeField]
    float waitTime = 1.0f;
        
    public float WaitTime => waitTime;

    //----------------------------------------------------------------------------------------------


    /// <summary>
    /// 현재 상태
    /// </summary>
    IState state;

    // 전체 상태들
    StateWait wait;     // 대기 상태
    StatePatrol patrol; // 순찰 상태
    StateChase chase;   // 추적 상태

    // 각종 컴포넌트들
    Animator animator;

    /// <summary>
    /// 현재 상태를 확인하기 위한 프로퍼티
    /// </summary>
    public IState State => state;

    public Animator Animator => animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        wait = new StateWait(this);
        patrol = new StatePatrol(this);
        chase = new StateChase(this);

        state = wait;   // 대기 상태를 현재 상태로 지정
    }

    private void Update()
    {
        state.Update(); // 현재 상태의 Update 함수 실행
    }

    /// <summary>
    /// 현재 상태를 다른 상태로 전이 시키는 함수
    /// </summary>
    /// <param name="target">전이할 상태</param>
    private void TransitionTo(IState target)
    {
        if(target != null)
        {
            state.Exit();   // 이전 상태의 Exit 실행
            state = target; // 상태를 target으로 변경하고
            state.Enter();  // 새 상태의 Enter 실행
        }
    }

    /// <summary>
    /// 현재 상태를 대기 상태로 전이시키는 함수
    /// </summary>
    public void TransitionToWait()
    {
        TransitionTo(wait);
    }

    /// <summary>
    /// 현재 상태를 순찰 상태로 전이시키는 함수
    /// </summary>
    public void TransitionToPatrol()
    {
        TransitionTo(patrol);
    }

    /// <summary>
    /// 현재 상태를 추적 상태로 전이시키는 함수
    /// </summary>
    public void TransitionToChase()
    {
        TransitionTo(chase);
    }
}
