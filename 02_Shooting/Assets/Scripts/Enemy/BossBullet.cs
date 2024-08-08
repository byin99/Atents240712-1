using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : RecycleObject
{
    // 항상 왼쪽으로 moveSpeed만큼 이동
    // 수명 있음
    // 플레이어와 충돌하면 터지는 이팩트 만들고 비활성화 됨

    /// <summary>
    /// 총알 이동 속도
    /// </summary>
    public float moveSpeed = 7.0f;

    /// <summary>
    /// 총알의 수명
    /// </summary>
    public float lifeTime = 20.0f;

    protected override void OnReset()
    {
        DisableTimer(lifeTime);
    }

    private void Update()
    {
        transform.Translate(Time.deltaTime * moveSpeed * Vector2.left);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Factory.Instance.GetHitEffect(transform.position);
            gameObject.SetActive(false);
        }
    }
}
