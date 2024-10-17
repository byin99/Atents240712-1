using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour, IInitializable
{
    Inventory inventory;
    public Inventory Inventory => inventory;

    /// <summary>
    /// 아이템을 줏을 수 있는 거리
    /// </summary>
    public float itemPickupRange = 2.0f;

    public void Initialize()
    {
        Player player = GetComponent<Player>();
        inventory = new Inventory(player);
        GameManager.Instance.InventoryUI.InitializeInventory(inventory);
    }

    /// <summary>
    /// 주변 아이템을 줍는 입력이 들어왔을 때 실행될 함수
    /// </summary>
    public void GetPickupItems()
    {
        // 주변에 있는 아이템(Item레이어로 되어있다)을 모두 획득해서 인벤토리에 추가하기
        Collider[] itemColliders = Physics.OverlapSphere(transform.position, itemPickupRange, LayerMask.GetMask("Item"));
        foreach (Collider collider in itemColliders)
        {
            ItemObject item = collider.GetComponent<ItemObject>();

            if (item != null)   // null이 아니면 ItemObject라는 이야기
            {
                if(Inventory.AddItem(item.ItemData.code))   // 인벤토리에 추가 시도
                {
                    item.ItemCollected();   // 추가에 성공했으면 비활성화시켜서 풀에 되돌리기
                }
            }
        }
    }
}
