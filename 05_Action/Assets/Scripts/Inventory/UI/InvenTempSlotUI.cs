using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InvenTempSlotUI : SlotUI_Base
{
    public override void InitializeSlot(InvenSlot slot)
    {
        base.InitializeSlot(slot);

        //GameManager.Instance.InventoryUI.Owner
    }

    private void Update()
    {
        // 항상 마우스 위치에 임시 슬롯을 위치하게 만들기(아이템이 임시 슬롯에 들어왔을 때만 보인다)
        transform.position = Mouse.current.position.ReadValue();
    }

    /// <summary>
    /// screen좌표가 가리키는 월드 포지션에 임시 슬롯에 들어있는 아이템을 드랍하는 함수
    /// </summary>
    /// <param name="screen"></param>
    public void ItemDrop(Vector2 screen)
    {
        // 1. 아이템이 있을 때만 처리
        // 2. scrren좌표가 가리키는 바닥(Ground) 주변에 아이템을 드랍
        // 3. 플레이어 위치에서 itemPickupRange반경안에 아이템이 드랍되어야 한다.
        // 4. 아이템을 1개 드랍할 때는 노이즈 없음

        
    }
}
