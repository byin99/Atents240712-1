using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class DetailInfoUI : MonoBehaviour
{
    public float alphaChangeSpeed = 10.0f;
    // 마우스 커서가 아이템이 들어있는 슬롯 위에 올라갔을 때 열린다
    // 열릴 때 ItemData를 확인해서 UI의 정보를 채운다.
    // 마우스 커서가 슬롯 밖으로 나갈 때 닫힌다.
    // 닫힐 때 alphaChangeSpeed의 속도로 canvasGroup의 alpha가 1 -> 0으로 감소한다.
    // DetailInfo는 아이템이 있는 슬롯 위에 있을 때 마우스 커서를 따라 다녀야 한다.
    // DetailInfo의 영역이 화면 밖으로 벗어나지 않게 만들어야 한다.
    // 아이템을 옮기는 도중에는 DetailInfo 창이 보이지 않아야 한다.

    public void Open(ItemData itemData)
    {
    }

    public void Close()
    { 
    }

    public void MovePosition(Vector2 screen)
    {

    }
}
