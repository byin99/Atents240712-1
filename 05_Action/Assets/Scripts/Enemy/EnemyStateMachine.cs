using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : MonoBehaviour
{ 
    /// <summary>
    /// 대기 상태로 들어갔을 때 기다리는 시간
    /// </summary>
    public float waitTime = 1.0f;

    /// <summary>
    /// 이동 속도
    /// </summary>
    public float moveSpeed = 3.0f;

    /// <summary>
    /// 적이 순찰할 웨이포인트(사실상 private)
    /// </summary>
    public Waypoints waypoints;

    /// <summary>
    /// 적의 현재 상태
    /// </summary>
    IState state = null;

    /// <summary>
    /// 대기 시간 측정용 변수(계속 감소)
    /// </summary>
    float waitTimer = 1.0f;


    /// <summary>
    /// 적의 현재 상태를 설정하고 확인하기 위한 프로퍼티
    /// </summary>
    IState State
    {
        get => state;
        set
        {
            if(state != value)
            {
                state.Exit();
                state = value;
                state.Enter();
            }
        }
    }

    WaitState waitState;
    PatrolState patrolState;

    NavMeshAgent agent;
    Animator animator;

    public float WaitTime => waitTime;

    public Waypoints Waypoints => waypoints;

    public NavMeshAgent Agent => agent;
    public Animator Animator => animator;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        waitState = new WaitState(this);
        patrolState = new PatrolState(this);
        waitState.onTransitionEvent += OnTransitionEvent;
        patrolState.onTransitionEvent += OnTransitionEvent;

        state = waitState;  // 처음이라
        state.Enter();
    }

    private void OnTransitionEvent(EnemyState targetState)
    {
        State = GetState(targetState);
    }

    private void Update()
    {
        state.Update();        
    }

    IState GetState(EnemyState state)
    {
        IState result = null;
        switch (state)
        {
            case EnemyState.Wait:
                result = waitState;
                break;
            case EnemyState.Patrol:
                result = patrolState;
                break;
            case EnemyState.Chase:
                break;
            case EnemyState.Attack:
                break;
            case EnemyState.Dead:
                break;
        }

        return result;
    }
}
