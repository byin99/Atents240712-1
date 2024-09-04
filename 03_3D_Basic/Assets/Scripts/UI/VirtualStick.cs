using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class VirtualStick : MonoBehaviour, IDragHandler, IEndDragHandler
{
    /// <summary>
    /// 핸들의 rect
    /// </summary>
    RectTransform handle;

    /// <summary>
    /// 핸들이 움직일 배경의 rect
    /// </summary>
    RectTransform background;

    void Awake()
    {
        background = transform as RectTransform;

        Transform child = transform.GetChild(0);
        handle = child as RectTransform;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log(eventData.position);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            background,                 // background 영역의 원점 기준으로
            eventData.position,         // 이 스크린 좌표가
            eventData.pressEventCamera, // (이 카메라 기준으로)
            out Vector2 localMove);     // 이만큼 움직였다(로컬좌표)

        // 핸들은 배경영역을 벗어나지 않아야 한다.

        //Debug.Log(localMove);
        InputUpdate(localMove);        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // 드래그가 끝났을 때 핸들은 중립위치(첫위치)로 돌아가야 한다.
    }

    void InputUpdate(Vector2 inputDelta)
    {
        // 움직임 처리
        handle.anchoredPosition = inputDelta;
    }
}
