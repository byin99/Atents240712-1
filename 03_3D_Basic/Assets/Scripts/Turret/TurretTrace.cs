using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    /// 추적할 플레이어
    /// </summary>
    Transform player;

    
}
