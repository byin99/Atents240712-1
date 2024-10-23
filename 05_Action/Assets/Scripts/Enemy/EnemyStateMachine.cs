using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.Rendering.DebugUI;

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
    /// 원거리 시야 범위
    /// </summary>
    public float farSightRange = 10.0f;

    /// <summary>
    /// 원거리 시야각의 절반
    /// </summary>
    public float sightHalfAngle = 50.0f;

    /// <summary>
    /// 근거리 시야 범위
    /// </summary>
    public float nearSightRange = 1.5f;

    /// <summary>
    /// 공격력(변수는 인스펙터에서 수정하기 위해 public으로 만든 것임)
    /// </summary>
    public float attackPower = 10.0f;
    public float AttackPower => attackPower;

    /// <summary>
    /// 방어력(변수는 인스펙터에서 수정하기 위해 public으로 만든 것임)
    /// </summary>
    public float defencePower = 3.0f;
    public float DefencePower => defencePower;

    /// <summary>
    /// 공격 속도
    /// </summary>
    public float attackInterval = 1.0f;


    /// <summary>
    /// 적의 현재 상태
    /// </summary>
    IState state = null;

    // 상태머신에서 사용되는 상태들
    WaitState waitState;
    PatrolState patrolState;
    ChaseState chaseState;

    // 자주 사용되는 컴포넌트들
    NavMeshAgent agent;
    Animator animator;

    public float WaitTime => waitTime;

    public Waypoints Waypoints => waypoints;

    public float AttackInterval => attackInterval;

    public NavMeshAgent Agent => agent;
    public Animator Animator => animator;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        agent.speed = moveSpeed;    // 그냥 시작할 때 이동 속도 지정
    }

    private void Start()
    {
        waitState = new WaitState(this);
        patrolState = new PatrolState(this);
        chaseState = new ChaseState(this);

        state = waitState;  // 첫 상태 설정
        
    }

    private void Update()
    {
        state.Update();        
    }

    void TransitionTo(IState targetState)
    {
        if (state != targetState)
        {
            state.Exit();
            state = targetState;
            state.Enter();
        }
    }

    public void TransitionToWait()
    {
        TransitionTo(waitState);
    }

    public void TransitionToPatrol()
    { 
        TransitionTo(patrolState); 
    }

    public void TransitionToChase()
    {
        TransitionTo(chaseState);
    }

    public bool SearchPlayer(out Vector3 target)
    {
        bool result = false;
        target = Vector3.zero;

        // 일정 반경(=farSightRange)안에 있는 플레이어 레이어에 있는 오브젝트 전부 찾기
        Collider[] colliders = Physics.OverlapSphere(transform.position, farSightRange, LayerMask.GetMask("Player"));
        if (colliders.Length > 0)
        {
            IHealth health = colliders[0].GetComponent<IHealth>();
            if (health != null && health.IsAlive)   // 플레이어가 살아있을 때만 찾기
            {
                // 일정 반경(=farSightRange)안에 플레이어가 있다.
                Vector3 playerPos = colliders[0].transform.position;    // 0번이 무조건 플레이어다(플레이어는 1명이니까)
                Vector3 toPlayerDir = playerPos - transform.position;   // 적->플레이어로 가는 방향 백터
                if (toPlayerDir.sqrMagnitude < nearSightRange * nearSightRange)  // 플레이어는 nearSightRange보다 안쪽에 있다.
                {
                    // 근접범위(=nearSightRange) 안쪽이다.
                    target = colliders[0].transform.position;
                    result = true;
                }
                else
                {
                    // 근접범위 밖이다 => 시야각 확인
                    if (IsInSightAngle(toPlayerDir))     // 시야각 안인지 확인
                    {
                        if (IsSightClear(toPlayerDir))   // 적과 플레이어 사이에 시야를 가리는 오브젝트가 있는지 확인
                        {
                            target = colliders[0].transform.position;
                            result = true;
                        }
                    }
                }
            }
        }

        return result;
    }

    /// <summary>
    /// 시야각(-sightHalfAngle ~ +sightHalfAngle)안에 플레이어가 있는지 없는지 확인하는 함수
    /// </summary>
    /// <param name="toTargetDirection">적에서 대상으로 향하는 방향 백터</param>
    /// <returns>시야각 안에 있으면 true, 없으면 false</returns>
    bool IsInSightAngle(Vector3 toTargetDirection)
    {
        float angle = Vector3.Angle(transform.forward, toTargetDirection);  // 적의 포워드와 적을 바라보는 방향백터 사이의 각을 구함
        return sightHalfAngle > angle;
    }

    /// <summary>
    /// 적이 다른 오브젝트에 의해 가려지는지 아닌지 확인하는 함수
    /// </summary>
    /// <param name="toTargetDirection">적에서 대상으로 향하는 방향 백터</param>
    /// <returns>true면 가려지지 않는다. false면 가려진다.</returns>
    bool IsSightClear(Vector3 toTargetDirection)
    {
        bool result = false;
        Ray ray = new(transform.position + transform.up * 0.5f, toTargetDirection); // 래이 생성(눈 높이 때문에 조금 높임)
        if (Physics.Raycast(ray, out RaycastHit hitInfo, farSightRange))
        {
            if (hitInfo.collider.CompareTag("Player"))   // 처음 충돌한 것이 플레이어라면
            {
                result = true;                          // 중간에 가리는 물체가 없다는 소리
            }
        }

        return result;
    }


#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        bool playerShow = SearchPlayer(out Vector3 target);
        Handles.color = playerShow ? Color.red : Color.green;

        Vector3 forward = transform.forward * farSightRange;
        Handles.DrawDottedLine(transform.position, transform.position + forward, 2.0f); // 중심선 그리기

        Quaternion q1 = Quaternion.AngleAxis(-sightHalfAngle, transform.up);            // 중심선 회전시키고
        Handles.DrawLine(transform.position, transform.position + q1 * forward);        // 선 긋기

        Quaternion q2 = Quaternion.AngleAxis(sightHalfAngle, transform.up);
        Handles.DrawLine(transform.position, transform.position + q2 * forward);

        Handles.DrawWireArc(transform.position, transform.up, q1 * forward, sightHalfAngle * 2, farSightRange, 2.0f);   // 호 그리기

        Handles.DrawWireDisc(transform.position, transform.up, nearSightRange);         // 근거리 시야 범위 그리기
    }

#endif
}
