using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // 플레이어가 WASD입력을 받아서 이동한다.(액션 이름은 Movement)
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

    /// <summary>
    /// 회전 방향(음수면 좌회전, 양수면 우회전)
    /// </summary>
    private float rotateDirection = 0.0f;

    /// <summary>
    /// 이동방향(양수면 전진, 음수면 후진)
    /// </summary>
    private float moveDirection = 0.0f;


    Rigidbody rigid;


    private void Awake()
    {
        inputActions = new PlayerInputActions();
        rigid = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += OnMoveInput;
        inputActions.Player.Move.canceled += OnMoveInput;
    }

    private void OnDisable()
    {
        inputActions.Player.Move.canceled -= OnMoveInput;
        inputActions.Player.Move.performed -= OnMoveInput;
        inputActions.Player.Disable();
    }

    private void OnMoveInput(InputAction.CallbackContext context)
    {
        SetInput(context.ReadValue<Vector2>(), !context.canceled);  // 입력 받은 내용 처리
    }

    private void FixedUpdate()
    {
        Movement(Time.fixedDeltaTime);  // 이동 및 회전
    }

    /// <summary>
    /// 이동 및 회전 처리용 함수
    /// </summary>
    private void Movement(float deltaTime)
    {
        // 새 이동할 위치 : 현재위치 + (초당 moveSpeed의 속도로, 오브젝트의 앞쪽 방향을 기준으로 전진/후진/정지)
        Vector3 position = rigid.position + deltaTime * moveSpeed * moveDirection * transform.forward;

        // 새 회전 : 현재 회전에서 추가로 (초당 rotateSpeed의 속도로, 오브젝트의 up을 축으로 좌회전/우회전/정지하는 회전) 
        Quaternion rotation = rigid.rotation * Quaternion.AngleAxis(deltaTime * rotateSpeed * rotateDirection, transform.up);

        rigid.Move(position, rotation);
    }


    /// <summary>
    /// 이동 입력 처리용 함수
    /// </summary>
    /// <param name="input">입력된 방향</param>
    /// <param name="isMove">이동 중이면 true, 아니면 false</param>
    void SetInput(Vector2 input, bool isMove)
    {
        rotateDirection = input.x;
        moveDirection = input.y;
    }
}
