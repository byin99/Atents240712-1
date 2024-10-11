using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CanvasGroup))]
public class InventoryUI : MonoBehaviour
{
    /// <summary>
    /// 이 UI가 보여줄 인벤토리
    /// </summary>
    Inventory inven;

    /// <summary>
    /// 인벤토리에 들어있는 Slot들의 UI 모음
    /// </summary>
    InvenSlotUI[] slotsUIs;

    /// <summary>
    /// 임시 슬롯의 UI
    /// </summary>
    InvenTempSlotUI tempSlotUI;

    // 입력 처리용
    PlayerInputActions inputActions;

    // On/Off용
    CanvasGroup canvasGroup;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        canvasGroup = GetComponent<CanvasGroup>();

        Transform child = transform.GetChild(0);
        slotsUIs = child.GetComponentsInChildren<InvenSlotUI>();

        child = transform.GetChild(1);
        Button close = child.GetComponent<Button>();
        close.onClick.AddListener(Close);

        child = transform.GetChild(2);
        tempSlotUI = child.GetComponent<InvenTempSlotUI>();
    }

    private void OnEnable()
    {
        inputActions.UI.Enable();
        inputActions.UI.InvenOnOff.performed += OnInvenOnOff;
        inputActions.UI.Click.canceled += OnItemDrop;
    }

    private void OnDisable()
    {
        inputActions.UI.Click.canceled -= OnItemDrop;
        inputActions.UI.InvenOnOff.performed -= OnInvenOnOff;
        inputActions.UI.Disable();
    }

    public void InitializeInventory(Inventory inventory)
    {
        inven = inventory;                              // 인벤토리 저장
        for(uint i = 0; i < slotsUIs.Length; i++)
        {
            slotsUIs[i].InitializeSlot(inven[i]);       // SlotUI 초기화
            slotsUIs[i].onDragBegin += OnItemMoveBegin;
            slotsUIs[i].onDragEnd += OnItemMoveEnd;
        }
        tempSlotUI.InitializeSlot(inven.TempSlot);      // TempSlotUI 초기화

        // Close();     // 임시 주석 처리
    }

    /// <summary>
    /// 슬롯에서 드래그가 시작되었을 때 실행되는 함수
    /// </summary>
    /// <param name="index">드래그가 시작된 슬롯의 인덱스</param>
    private void OnItemMoveBegin(uint index)
    {
        inven.MoveItem(index, tempSlotUI.Index);
    }

    /// <summary>
    /// 슬롯에서 드래그가 끝났을 때 실행되는 함수
    /// </summary>
    /// <param name="index">드래그가 끝난 슬롯의 index(null이면 드래그가 비정상적으로 끝난 경우)</param>
    private void OnItemMoveEnd(uint? index)
    {
        if (index.HasValue)
        {
            inven.MoveItem(tempSlotUI.Index, index.Value);
        }
    }

    private void OnInvenOnOff(InputAction.CallbackContext _)
    {
        if(canvasGroup.interactable)
        {
            Close();
        }
        else
        {
            Open();
        }
    }

    private void OnItemDrop(InputAction.CallbackContext _)
    {
    }

    void Open()
    {
        canvasGroup.alpha = 1.0f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    void Close()
    {
        canvasGroup.alpha = 0.0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

}
