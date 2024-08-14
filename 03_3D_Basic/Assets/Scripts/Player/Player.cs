using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 플레이어가 WASD입력을 받아서 이동한다.(액션 이름은 Move)
    // WS로 전진/후진
    // AD로 좌회전/우회전
    // 실제 이동 처리는 Rigidbody를 이용해서 처리

    /// <summary>
    /// 플레이어 이동 속도
    /// </summary>
    public float moveSpeed = 5.0f;

    /// <summary>
    /// 플레이어 회전 속도
    /// </summary>
    public float rotateSpeed = 180.0f;

    /// <summary>
    /// 인풋 액션
    /// </summary>
    PlayerInputActions inputActions;
}
