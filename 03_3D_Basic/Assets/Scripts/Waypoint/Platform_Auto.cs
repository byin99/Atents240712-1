using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Auto : PlatformBase
{
    // 플레이어가 플랫폼 위에 올라오면 반대쪽으로 이동하는 플랫폼

    /// <summary>
    /// 플랫폼 이동여부를 결정하는 변수(true면 정지, false면 이동)
    /// </summary>
    bool isPause = true;

    protected override void Start()
    {
        base.Start();
        Target = targetWaypoints.GetNextWaypoint(); // 시작했을 때 첫번째로 Point2로 이동하게끔 설정
    }

    protected override void OnMove(Vector3 moveDelta)
    {
        if(!isPause)
        {
            base.OnMove(moveDelta);
        }
    }

    protected override void RiderOn(IPlatformRide target)
    {
        base.RiderOn(target);
        isPause = false;
    }

    protected override void OnArrived()
    {
        isPause = true;
        base.OnArrived();
    }
}
