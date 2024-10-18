using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour, IInitializable, IMoneyContainer
{
    /// <summary>
    /// 실제 인벤토리 데이터
    /// </summary>
    Inventory inventory;

    /// <summary>
    /// 아이템을 줏을 수 있는 거리
    /// </summary>
    public float itemPickupRange = 2.0f;

    /// <summary>
    /// 이 인벤토리에 저장된 돈
    /// </summary>
    int money = 0;

    /// <summary>
    /// 인벤토리를 읽기 위한 프로퍼티
    /// </summary>
    public Inventory Inventory => inventory;

    /// <summary>
    /// 이 인벤토리에 저장된 돈에 접근하기 위한 프로퍼티
    /// </summary>
    public int Money 
    { 
        get => money;
        set
        {
            if (money != value)
            {
                money = value;
                onMoneyChange?.Invoke(money);
            }
        }
    }

    /// <summary>
    /// 돈에 변화가 있을 때 변화된 금액을 알리는 델리게이트(int:최종 변경 값)
    /// </summary>
    public event Action<int> onMoneyChange;

    /// <summary>
    /// 인벤토리 초기화 함수
    /// </summary>
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
                IConsumable consumable = item.ItemData as IConsumable;
                if (consumable != null)
                {
                    // 즉시 소비되는 아이템
                    consumable.Consume(gameObject); // 플레이어에게 즉시 사용
                    item.ItemCollected();
                }
                else
                {
                    // 인벤토리로 들어가는 아이템
                    if (Inventory.AddItem(item.ItemData.code))   // 인벤토리에 추가 시도
                    {
                        item.ItemCollected();   // 추가에 성공했으면 비활성화시켜서 풀에 되돌리기
                    }
                }                
            }
        }
    }
}
