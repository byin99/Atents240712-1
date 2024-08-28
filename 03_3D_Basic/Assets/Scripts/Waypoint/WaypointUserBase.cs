using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointUserBase : MonoBehaviour
{
    /// <summary>
    /// 이 오브젝트가 따라 움직일 경로를 가진 웨이포인트
    /// </summary>
    public Waypoints targetWaypoints;

    /// <summary>
    /// 이동 속도
    /// </summary>
    public float moveSpeed = 5.0f;

    /// <summary>
    /// 오브젝트의 이동 방향
    /// </summary>
    Vector3 moveDirection;

    /// <summary>
    /// 현재 목표로 하고 있는 웨이포인트 지점의 트랜스폼
    /// </summary>
    Transform target;

    /// <summary>
    /// 목표로할 웨이포인트를 지정하고 확인하는 프로퍼티
    /// </summary>
    protected virtual Transform Target
    {
        get => target;
        set
        {
            target = value;
            moveDirection = (target.position - transform.position).normalized;
        }
    }

    /// <summary>
    /// 현재 목표지점에 근접했는지 확인해주는 프로퍼티(true면 도착, false면 도착하지 않음)
    /// </summary>
    bool IsArrived
    {
        get
        {
            return (target.position - transform.position).sqrMagnitude < 0.01f; // 도착지점까지의 거리가 0.1보다 작으면 도착했다고 판단
        }
    }
}
