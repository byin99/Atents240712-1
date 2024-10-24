using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

#if UNITY_EDITOR
using UnityEditor;
#endif

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



    // 플레이어 탐색 용 변수들 -----------------------------------------------------------------------

    /// <summary>
    /// 원거리 시야 범위
    /// </summary>
    [SerializeField]
    private float farSightRange = 10.0f;

    /// <summary>
    /// 원거리 시야각의 절반
    /// </summary>
    [SerializeField]
    private float sightHalfAngle = 60.0f;

    /// <summary>
    /// 근거리 시야 범위
    /// </summary>
    [SerializeField]
    private float nearSightRange = 1.5f;
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
    NavMeshAgent agent;

    /// <summary>
    /// 현재 상태를 확인하기 위한 프로퍼티
    /// </summary>
    public IState State => state;

    // 컴포넌트들에 접근하기 위한 프로퍼티들
    public Animator Animator => animator;
    public NavMeshAgent Agent => agent;

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

    /// <summary>
    /// 플레이어를 탐색하는 함수
    /// </summary>
    /// <param name="position">플레이어의 위치(발견했을 때)</param>
    /// <returns>true면 발견했다, false면 발견하지 못했다</returns>
    public bool SearchPlayer(out Vector3 position)
    {
        bool result = false;
        position = Vector3.zero;
        return result;
    }

    /// <summary>
    /// 시야각 안에 플레이어가 있는지 확인하는 함수
    /// </summary>
    /// <param name="toTargetDir">슬라임이 플레이어를 바라보는 방향 벡터</param>
    /// <returns>시야각 안에 있으면 true, 없으면 false</returns>
    bool IsInSightAngle(Vector3 toTargetDir)
    {
        return false;
    }

    /// <summary>
    /// 플레이어가 다른 오브젝트에 의해 가려지는지 아닌지 확인하는 함수
    /// </summary>
    /// <param name="toTargetDir">슬라임이 플레이어를 바라보는 방향 벡터</param>
    /// <returns>true면 슬라임과 플레이어 사이에 다른 오브젝트가 없다. false면 다른 오브젝트가 가리고 있다.</returns>
    bool IsSightClear(Vector3 toTargetDir)
    {
        return false;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        bool isPlayerShow = SearchPlayer(out Vector3 _);
        Handles.color = isPlayerShow ? Color.red : Color.blue;

        Handles.DrawWireDisc(transform.position, transform.up, nearSightRange, 5.0f);   // 근거리 시야범위 표시

        Vector3 forward = transform.forward * farSightRange;
        Handles.DrawDottedLine(transform.position, transform.position + forward, 2.0f); // 원거리 중심선 그리기

        Quaternion q1 = Quaternion.AngleAxis(-sightHalfAngle, transform.up);
        Handles.DrawLine(transform.position, transform.position + q1 * forward, 3.0f);  // 부채꼴의 왼쪽 직선

        Quaternion q2 = Quaternion.AngleAxis(sightHalfAngle, transform.up);
        Handles.DrawLine(transform.position, transform.position + q2 * forward, 3.0f);  // 부채꼴의 오른쪽 직선

        Handles.DrawWireArc(transform.position, transform.up, q1 * forward, sightHalfAngle * 2.0f, farSightRange, 3.0f);    // 부채꼴의 호
    }
#endif
}
