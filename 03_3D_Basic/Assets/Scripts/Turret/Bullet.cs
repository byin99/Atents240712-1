using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : RecycleObject
{
    /// <summary>
    /// 초기 속도
    /// </summary>
    public float initialSpeed = 20.0f;

    /// <summary>
    /// 총알 수명
    /// </summary>
    public float lifeTime = 10.0f;

    Rigidbody rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    protected override void OnReset()
    {
        DisableTimer(lifeTime);     // 수명 설정

        // ???

        rigid.velocity = initialSpeed * transform.forward;  // 앞으로 날아가게 만들기
    }

    private void OnCollisionEnter(Collision collision)
    {
        StopAllCoroutines();    // 부딪치면 이전 코루틴 정지
        DisableTimer(2.0f);     // 새로 2초뒤에 사라지기
    }

    // 총알이 날아갈 때 앞으로 기울어지게 만들기
}
