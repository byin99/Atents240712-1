using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : EnemyBase
{
    [Header("보스 데이터")]
    // 등장하고 위아래로 왕복한다
    // 총알은 주기적으로 발사(Fire1, Fire2 위치)
    // 미사일은 방향전환을 할 때마다 일정 수(barrageCount)만큼 연사(Fire3위치)

    /// <summary>
    /// 총알 발사 간격
    /// </summary>
    public float bulletInterval = 1.0f;

    /// <summary>
    /// 미사일 일제발사 때 발사별 간격
    /// </summary>
    public float barrageInterval = 0.2f;

    /// <summary>
    /// 미사일 일제발사 때 발사 회수
    /// </summary>
    public int barrageCount = 3;

    /// <summary>
    /// 보스 활동 영역 최소 위치
    /// </summary>
    public Vector2 areaMin = new Vector2(2, -3);

    /// <summary>
    /// 보스 활동 영역 최대 위치
    /// </summary>
    public Vector2 areaMax = new Vector2(7, 3);

    /// <summary>
    /// 총알 발사 위치1
    /// </summary>
    Transform fire1;

    /// <summary>
    /// 총알 발사 위치2
    /// </summary>
    Transform fire2;

    /// <summary>
    /// 총알 발사 위치3
    /// </summary>
    Transform fire3;

    /// <summary>
    /// 보스 이동 방향
    /// </summary>
    Vector3 moveDirection = Vector3.left;

    private void Awake()
    {
        Transform fire = transform.GetChild(1);
        fire1 = fire.GetChild(0);
        fire2 = fire.GetChild(1);
        fire3 = fire.GetChild(2);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Vector3 p0 = new(areaMin.x, areaMin.y);
        Vector3 p1 = new(areaMax.x, areaMin.y);
        Vector3 p2 = new(areaMax.x, areaMax.y);
        Vector3 p3 = new(areaMin.x, areaMax.y);

        Gizmos.DrawLine(p0, p1);
        Gizmos.DrawLine(p1, p2);
        Gizmos.DrawLine(p2, p3);
        Gizmos.DrawLine(p3, p0);
    }

    protected override void OnMoveUpdate(float deltaTime)
    {
        transform.Translate(deltaTime * moveSpeed * moveDirection); // 항상 moveDirection 방향으로 이동
    }

    protected override void OnReset()
    {
        base.OnReset();

        StartCoroutine(MovePaternProcess());
    }

    IEnumerator MovePaternProcess()
    {
        moveDirection = Vector3.left;   // 기본방향은 왼쪽

        yield return null;      // 풀에서 꺼냈을 때 OnReset이 먼저 실행된 후 위치 설정을 하기 때문에,
                                // 위치 설정 이후에 아래코드가 실행되도록 한 프레임 대기

        float middleX = (areaMax.x - areaMin.x) * 0.5f + areaMin.x; // area의 가운데 구하기
        while (transform.position.x > middleX)
        {
            yield return null;  // 보스의 x 위치가 middleX의 왼쪽이 될때까지 대기
        }

        // Debug.Log("가운데 도착");
        StartCoroutine(FireBulletCoroutine());  // 총알 계속 발사
        ChangeDirection();                      // 일단 아래로 이동

        while(true)
        {
            if(transform.position.y > areaMax.y || transform.position.y < areaMin.y)    // 범위를 벗어나면
            {
                ChangeDirection();                      // 방향 전환
                StartCoroutine(FireMissleCoroutine());  // 미사일 발사
            }
            yield return null;
        }
    }

    IEnumerator FireBulletCoroutine()
    {
        while (true)
        {
            // 총알 발사 간격에 따라 계속 총알 발사
            Factory.Instance.GetBossBullet(fire1.position);
            Factory.Instance.GetBossBullet(fire2.position);

            yield return new WaitForSeconds(bulletInterval);
        }
    }

    IEnumerator FireMissleCoroutine()
    {
        for (int i = 0;i<barrageCount;i++)
        {
            Factory.Instance.GetBossMisslie(fire3.position);    // 연속 발사 개수만큼 미사일 생성
            yield return new WaitForSeconds(barrageInterval);   
        }
    }

    /// <summary>
    /// 보스의 이동 방향을 변경하는 함수(반드시 위아래 이동 범위를 벗어났을 때만 실행되어야 함)
    /// </summary>
    void ChangeDirection()
    {
        Vector3 target = new Vector3(); ;
        target.x = Random.Range(areaMin.x, areaMax.x);                          // x위치는 areaMin.x ~ areaMax.x 사이
        target.y = (transform.position.y > areaMax.y) ? areaMin.y : areaMax.y;  // areaMax보다 위로 갔으면 아래줄, 아니면 윗줄(위나 아래를 벗어났을 때만 실행이 되므로)

        moveDirection = (target - transform.position).normalized;   // 방향 변경(target으로 가는 방향)

    }
}
