using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /// <summary>
    /// 입력된 이동 방향(3D 공간의 이동 방향, y는 무조건 바닥 높이)
    /// </summary>
    Vector3 inputDirection  = Vector3.zero;

    // 컴포넌트 들
    CharacterController characterController;
    PlayerInputController inputController;


    private void Awake()
    {
        characterController = GetComponent<CharacterController>();

        inputController = GetComponent<PlayerInputController>();
        inputController.onMove += OnMoveInput;
        inputController.onMoveModeChange += OnMoveModeChangeInput;
    }

    private void Update()
    {
        characterController.Move(Time.deltaTime * inputDirection);  // 수동
        //characterController.SimpleMove(inputDirection);             // 자동
    }

    /// <summary>
    /// 이동입력에 대한 델리게이트로 실행되는 함수
    /// </summary>
    /// <param name="input">입력된 방향</param>
    /// <param name="isPress">키를 눌렀으면 true, 땠으면 false</param>
    private void OnMoveInput(Vector2 input, bool isPress)
    {
        // inputDirection 설정
        // 입력에 따라 카메라 기준으로 앞뒤좌우로 이동한다.
    }

    private void OnMoveModeChangeInput()
    {
        
    }
}
