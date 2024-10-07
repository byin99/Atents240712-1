using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    /// <summary>
    /// 인벤토리의 기본 슬롯 갯수(6칸)
    /// </summary>
    const int Default_Inventory_Size = 6;

    /// <summary>
    /// 인벤토리의 슬롯들(아이템 한종류가 들어간다)
    /// </summary>
    InvenSlot[] slots;

    /// <summary>
    /// 인벤토리의 슬롯의 갯수
    /// </summary>
    int SlotCount => slots.Length;

    /// <summary>
    /// 임시 슬롯(드래그나 아이템 분리작업에서 사용)
    /// </summary>
    InvenTempSlot tempSlot;

    /// <summary>
    /// 아이템 데이터 매니저(아이템 종류별 정보를 전부 가지고 있다.)
    /// </summary>
    ItemDataManager itemDataManager;

    /// <summary>
    /// 인벤토리의 수요자
    /// </summary>
    Player owner;

    /// <summary>
    /// 소유자 확인용 프로퍼티
    /// </summary>
    public Player Owner => owner;

    /// <summary>
    /// 인벤토리 슬롯에 접근하기 위한 인덱서
    /// </summary>
    /// <param name="index">슬롯의 인덱스</param>
    /// <returns>인덱스 번째의 슬롯</returns>
    public InvenSlot this[uint index] => slots[index];

    /// <summary>
    /// 임시 슬롯 확인용 프로퍼티
    /// </summary>
    public InvenTempSlot TempSlot => tempSlot;

    /// <summary>
    /// 인벤토리 생성자
    /// </summary>
    /// <param name="owner">인벤토리의 소유자</param>
    /// <param name="size">인벤토리의 크기(기본값은 6)</param>
    public Inventory(Player owner, uint size = Default_Inventory_Size)
    {
        slots = new InvenSlot[size];
        for (uint i = 0; i < slots.Length; i++)
        {
            slots[i] = new InvenSlot(i);
        }
        tempSlot = new InvenTempSlot();
        itemDataManager = GameManager.Instance.ItemData;    // 타이밍 조심 필요

        this.owner = owner;
    }

    // 아이템 추가
    // 아이템 삭제(슬롯 일부 삭제, 슬롯 삭제, 인벤토리 전체 삭제)
    // 아이템 이동
    // 아이템 스왑
    // 아이템 덜어내서 임시슬롯에 저장
    // 인벤토리 내 아이템 정렬
    // 인벤토리 정리
    // 테스트 : 인벤토리 내용 출력
}
