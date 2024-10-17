using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInputController))]
[RequireComponent(typeof(PlayerMovement), typeof(PlayerAttack), typeof(PlayerInventory))]
public class Player : MonoBehaviour
{
    /// <summary>
    /// 인벤토리 확인용 프로퍼티(테스트용)
    /// </summary>
    public Inventory Inventory => inventory.Inventory;

    /// <summary>
    /// 아이템 획득가능 범위 확인용 프로퍼티(아이템을 버릴 수 있는 최대 거리)
    /// </summary>
    public float ItemPickupRange => inventory.itemPickupRange;

    // 컴포넌트 들
    CharacterController characterController;

    PlayerInputController inputController;
    PlayerMovement movement;
    PlayerAttack attack;
    PlayerInventory inventory;


    private void Awake()
    {
        characterController = GetComponent<CharacterController>();

        inputController = GetComponent<PlayerInputController>();
        movement = GetComponent<PlayerMovement>();
        attack = GetComponent<PlayerAttack>();
        inventory = GetComponent<PlayerInventory>();

        inputController.onMove += movement.SetDirection;
        inputController.onMoveModeChange += movement.ToggleMoveMode;
        inputController.onAttack += attack.OnAttackInput;
        inputController.onPickUp += inventory.GetPickupItems;
    }

    public void Initialize()
    {
        IInitializable[] inits = GetComponents<IInitializable>();
        foreach (var init in inits)
        {
            init.Initialize();
        }
    }
}
