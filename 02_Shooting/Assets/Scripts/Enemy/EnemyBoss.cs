using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : EnemyBase
{
    [Header("보스 데이터")]
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
}
