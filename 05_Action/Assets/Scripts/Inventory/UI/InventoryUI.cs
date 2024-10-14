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

    /// <summary>
    /// 상세 정보창
    /// </summary>
    DetailInfoUI detailInfoUI;

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

        child = transform.GetChild(3);
        detailInfoUI = child.GetComponent<DetailInfoUI>();
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
            slotsUIs[i].onClick += OnSlotClick;
            slotsUIs[i].onPointerEnter += OnItemDetailInfoOpen;
            slotsUIs[i].onPointerExit += OnItemDetailInfoClose;
            slotsUIs[i].onPointerMove += OnSlotPointerMove;
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
        detailInfoUI.IsPaused = true;       // 상세정보창 일시 정지
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
        detailInfoUI.IsPaused = false;      // 상세정보창 일시 정지 해제
    }

    /// <summary>
    /// 슬롯을 클릭했을 때 실행되는 함수
    /// </summary>
    /// <param name="index">클릭한 슬롯의 인덱스</param>
    private void OnSlotClick(uint index)
    {
    }

    /// <summary>
    /// 마우스 커서가 슬롯위에 들어갔을 때 상세 정보창을 여는 함수
    /// </summary>
    /// <param name="index">슬롯의 인덱스</param>
    private void OnItemDetailInfoOpen(uint index)
    {
        detailInfoUI.Open(slotsUIs[index].InvenSlot.ItemData);  // 열기
    }

    /// <summary>
    /// 마우스 커서가 슬롯을 나갔을 때 상세 정보창을 닫는 함수
    /// </summary>
    private void OnItemDetailInfoClose()
    {
        detailInfoUI.Close();
    }

    /// <summary>
    /// 마우스 커서가 슬롯 안에서 움직일 때 실행되는 함수
    /// </summary>
    /// <param name="screen">마우스 커서의 스크린 좌표</param>
    private void OnSlotPointerMove(Vector2 screen)
    {
        detailInfoUI.MovePosition(screen);
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
