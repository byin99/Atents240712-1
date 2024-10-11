#define PrintTestLog

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class InvenSlotUI : SlotUI_Base, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    /// <summary>
    /// 드래그의 시작을 알리는 델리게이트(uint:드래그를 시작한 슬롯의 인덱스)
    /// </summary>
    public event Action<uint> onDragBegin;

    /// <summary>
    /// 드래그의 종료를 알리는 델리게이트(uint?:드래그가 끝난 슬롯의 인덱스(null이면 슬롯이 아닌 곳에서 종료))
    /// </summary>
    public event Action<uint?> onDragEnd;

    /// <summary>
    /// 장비표시용 텍스트
    /// </summary>
    TextMeshProUGUI equipText;

    protected override void Awake()
    {
        base.Awake();
        Transform child = transform.GetChild(2);
        equipText = child.GetComponent<TextMeshProUGUI>();
    }

    protected override void OnRefresh()
    {
        base.OnRefresh();
        if( InvenSlot.IsEquipped)
        {
            equipText.color = Color.red;    // 장비했을 때는 빨간색
        }
        else
        {
            equipText.color = Color.clear;  // 장비하지 않았을 때는 투명색
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
#if PrintTestLog
        Debug.Log($"드래그 시작 : [{Index}]번 슬롯");
#endif
        onDragBegin?.Invoke(Index);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // OnBeginDrag와 OnEndDrag를 발동시키기 위해 필요
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject obj = eventData.pointerCurrentRaycast.gameObject;   // 드래그 끝난 위치에 있는 게임 오브젝트
        if (obj != null)
        {
            // 마우스 위치에 어떤 것이 있다.
            InvenSlotUI endSlot = obj.GetComponent<InvenSlotUI>();
            uint? endIndex = null;
            if (endSlot != null)
            {
                // 슬롯이다.
                endIndex = endSlot.Index;
#if PrintTestLog
                Debug.Log($"드래그 종료 : [{endIndex}]번 슬롯");
#endif
            }
#if PrintTestLog
            else
            {
                // 슬롯이 아니다.
                Debug.Log($"드래그 종료 : [{obj.name}]은 슬롯이 아닙니다.");
            }
#endif
            onDragEnd?.Invoke(endIndex);
        }
        else
        {
            // 마우스 위치에 어떤 게임 오브젝트도 없다.
#if PrintTestLog
            Debug.Log("드래그 종료 : 어떤 UI도 없다.");
#endif
        }
    }
}
