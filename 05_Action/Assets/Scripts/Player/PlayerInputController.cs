using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    /// <summary>
    /// 이동 입력을 전달하는 델리게이트(Vector2:이동방향, bool 누른 상황(true면 눌렀다))
    /// </summary>
    public Action<Vector2, bool> onMove;

    /// <summary>
    /// 이동 모드 변경 입력을 전달하는 델리게이트
    /// </summary>
    public Action onMoveModeChange;

    // 인풋 액션 에셋
    PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnMove;
        inputActions.Player.MoveModeChange.performed += OnMoveModeChange;

    }

    private void OnDisable()
    {
        inputActions.Player.MoveModeChange.performed -= OnMoveModeChange;
        inputActions.Player.Move.canceled -= OnMove;
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Disable();
    }

    private void OnMove(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        onMove?.Invoke(input, !context.canceled);
    }

    private void OnMoveModeChange(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        onMoveModeChange?.Invoke();
    }

}
