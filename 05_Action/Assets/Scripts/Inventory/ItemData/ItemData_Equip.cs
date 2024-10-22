using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData_Equip : ItemData, IEquipable
{
    [Header("장비 아이템 데이터")]
    /// <summary>
    /// 아이템을 장비 했을 때 생성해야 할 프리팹
    /// </summary>
    public GameObject equipPrefab;

    /// <summary>
    /// 아이템이 장비될 위치를 알려주는 프로퍼티
    /// </summary>
    public virtual EquipType EquipType => EquipType.Weapon;

    /// <summary>
    /// 아이템을 장비하는 함수
    /// </summary>
    /// <param name="target">장비받을 대상</param>
    /// <param name="slot">아이템이 들어있는 슬롯</param>
    public void Equip(GameObject target, InvenSlot slot)
    {
    }

    /// <summary>
    /// 아이템을 장비 해제하는 함수
    /// </summary>
    /// <param name="target">장비 해제할 대상</param>
    /// <param name="slot">아이템이 들어있는 슬롯</param>
    public void UnEquip(GameObject target, InvenSlot slot)
    {

    }

    /// <summary>
    /// 아이템을 장비 또는 해제하는 함수
    /// </summary>
    /// <param name="target">장비받거나 해제할 대상</param>
    /// <param name="slot">아이템이 들어있는 슬롯</param>
    public void ToggleEquip(GameObject target, InvenSlot slot)
    { 
    }
}
