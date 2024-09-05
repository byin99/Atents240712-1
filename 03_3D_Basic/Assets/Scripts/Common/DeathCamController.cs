using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeathCamController : MonoBehaviour
{
    /// <summary>
    /// 카트 최대 속도(시작할때의 속도)
    /// </summary>
    public float cartMaxSpeed = 10.0f;

    /// <summary>
    /// 카트 최저 속도
    /// </summary>
    public float cartMinSpeed = 3.0f;

    /// <summary>
    /// 카트의 속도가 최대에서 최저로 떨어지는데 걸리는 시간
    /// </summary>
    public float speedDecreaseDuration = 10.0f;

    /// <summary>
    /// 카트의 속도 변화를 나타내는 커브
    /// </summary>
    public AnimationCurve cartSpeedCurve;

    /// <summary>
    /// 사망 시작부터 얼마나 시간이 지났는지 기록하는 변수
    /// </summary>
    float elapsedTime = 0.0f;

    // 각종 컴포넌트
    CinemachineVirtualCamera vcam;
    CinemachineDollyCart cart;
    Player player;
    Transform playerCameraRoot;

    bool isStart = false;

    private void Awake()
    {
        vcam = GetComponentInChildren<CinemachineVirtualCamera>();
        cart = GetComponentInChildren<CinemachineDollyCart>();
    }

    private void Start()
    {
        player = GameManager.Instance.Player;
        playerCameraRoot = player.transform.GetChild(8);
        player.onDie += DeathCamStart;          // 플레이어가 죽으면 시작
    }

    private void Update()
    {
        if(isStart) // 플레이어가 죽었다는 신호가 들어오면
        {
            transform.position = playerCameraRoot.position; // 플레이어 카메라 루트 위치로 옮기고
            elapsedTime += Time.deltaTime;                  // 시간 누적 시작
            float ratio = cartSpeedCurve.Evaluate(elapsedTime/speedDecreaseDuration);   
            cart.m_Speed = cartMinSpeed + (cartMaxSpeed - cartMinSpeed)* ratio; // 카트 속도 조절
        }
    }

    /// <summary>
    /// 사망 카메라 연출 시작
    /// </summary>
    private void DeathCamStart()
    {
        isStart = true;                 // 시작되어야 한다고 표시
        vcam.Priority = 100;            // 카메라 우선 순위 높여서 이 카메라로 찍히게 만들기
        cart.m_Speed = cartMaxSpeed;    // 카트 속도 최대치로 변경
        cart.m_Position = 0;            // 카트 위치 리셋
        elapsedTime = 0.0f;             // 시작부터 시간이 얼마나 지났는지 측정하기 위한 값 리셋
    }
}
