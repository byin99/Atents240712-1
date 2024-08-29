using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlatformBase : WaypointUserBase
{
    /// <summary>
    /// 플랫폼이 움직일때마다 움직인 정도를 파라메터로 넘기는 델리게이트
    /// </summary>
    public Action<Vector3> onPlatformMove;

    protected override void Start()
    {
        if(targetWaypoints == null)
        {
            int nextIndex = transform.GetSiblingIndex() + 1;                // 동생의 인덱스 구하기
            Transform nextSibling = transform.parent.GetChild(nextIndex);   // 동생의 트랜스폼 구하기
            targetWaypoints = nextSibling.GetComponent<Waypoints>();        // 동생이 무조건 Waypoint를 가지고 있다는 전제
        }
        base.Start();
    }

    /// <summary>
    /// 이동 처리 + 이동 알림
    /// </summary>
    /// <param name="moveDelta">움직인 정도</param>
    protected override void OnMove(Vector3 moveDelta)
    {
        base.OnMove(moveDelta);
        onPlatformMove?.Invoke(moveDelta);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"OnTriggerEnter : {other.gameObject.name}");
        IPlatformRide target = other.GetComponent<IPlatformRide>();
        if(target != null)
        {
            Debug.Log($"등록 : {other.gameObject.name}");
            onPlatformMove += target.OnRidePlatform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IPlatformRide target = other.GetComponent<IPlatformRide>();
        if (target != null)
        {
            onPlatformMove -= target.OnRidePlatform;
        }
    }
}
