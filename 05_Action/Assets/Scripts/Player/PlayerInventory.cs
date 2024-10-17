using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour, IInitializable
{
    Inventory inventory;
    public Inventory Inventory => inventory;

    public void Initialize()
    {
        inventory = new Inventory(this);
        GameManager.Instance.InventoryUI.InitializeInventory(inventory);
    }

    /// <summary>
    /// 주변 아이템을 줍는 입력이 들어왔을 때 실행될 함수
    /// </summary>
    public void GetPickupItems()
    {
        // 주변에 있는 아이템(Item레이어로 되어있다)을 모두 획득해서 인벤토리에 추가하기
    }
}
