using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class TurretTrace : TurretBase
{
    // 사정거리(sightRange) 안에 플레이어가 들어오면 플레이어 방향으로 Gun이 회전한다.
    // 사정거리(sightRange) 안에 플레이어가 들어오면 계속 총알을 발사한다.

    // 시야각 적용해보기
    // 시야 가려짐 적용해보기

    /// <summary>
    /// 사정거리
    /// </summary>
    public float sightRange = 10.0f;

    /// <summary>
    /// 회전 속도용 모디파이어
    /// </summary>
    public float turnSmooth = 2.0f;

    /// <summary>
    /// 터렛이 총알 발사를 시작하는 좌우 발사각(10일 경우 +-10도 씩)
    /// </summary>
    public float fireAngle = 10.0f;

    /// <summary>
    /// 추적할 플레이어
    /// </summary>
    Transform target;

    /// <summary>
    /// 시야범위 체크용 트리거
    /// </summary>
    SphereCollider sightTrigger;

    /// <summary>
    /// 총알 발사 코루틴을 저장해 놓은 변수
    /// </summary>
    IEnumerator fireCoroutine;

    /// <summary>
    /// 현재 발사 중인지를 기록하는 변수
    /// </summary>
    bool isFiring = false;

    protected override void Awake()
    {
        base.Awake();

        sightTrigger = GetComponent<SphereCollider>();
        sightTrigger.radius = sightRange;
        fireCoroutine = PeriodFire();
    }

    private void Update()
    {
        LookTargetAndAttack();
    }

    private void OnTriggerEnter(Collider other)
    {        
        if(other.CompareTag("Player"))
        {
            target = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            target = null;
        }
    }

#if UNITY_EDITOR
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        // 시야 범위 그리기
        //Gizmos.DrawWireSphere(transform.position, sightRange);
        Handles.color = Color.white;
        Handles.DrawWireDisc(transform.position, transform.up, sightRange, 3.0f);

        // 총구 방향 그리기
        Handles.color = Color.yellow;
        if(gunTransform == null)
            gunTransform = transform.GetChild(2);   // 없으면 찾기
        Vector3 from = transform.position;
        Vector3 to = transform.position + gunTransform.forward * sightRange;
        Handles.DrawDottedLine(from, to, 2.0f);

        // 발사각 그리기
        // 일단 녹색
        //Handles.DrawWireArc(중심점, 위쪽방향, 시작 방향 벡터, 각도, 두깨);
    }
#endif

    void LookTargetAndAttack()
    {
        if(target != null)
        {
            Vector3 direction = target.transform.position - transform.position; // 플레이어를 바라보는 방향
            direction.y = 0.0f; // xz평면으로만 회전하게 하기 위해 y는 제거
            //gunTransform.forward = direction; // 즉시 이동

            gunTransform.rotation = Quaternion.Slerp(
                gunTransform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * turnSmooth);

            StartFire();
        }
        else
        {
            StopFire();
        }
    }

    /// <summary>
    /// 총알 발사 코루틴을 실행시키는 함수
    /// </summary>
    void StartFire()
    {
        if(!isFiring)   // 발사 중이 아닐 때만
        {
            isFiring = true;
            StartCoroutine(fireCoroutine);  // 발사 코루틴 실행
        }
    }

    /// <summary>
    /// 총알 발사 코루틴을 정지시키는 함수
    /// </summary>
    void StopFire()
    {
        if(isFiring)    // 발사 중일 때만
        {
            StopCoroutine(fireCoroutine);   // 발사 코루틴 정지
            isFiring = false;
        }
    }
}
