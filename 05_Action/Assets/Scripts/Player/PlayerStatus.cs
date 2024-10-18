using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour, IHealth
{
    // 1. HP와 MP가 존재
    // 2. 먹으면 HP와 MP가 천천히 증가하는 아이템 만들기(IConsumable 상속) - ItemData_Food, ItemData_Drink
    // 3. Food는 틱 단위로 회복
    // 4. Drink는 틱 없이 일정하게 회복
    // 5. 인스팩터 창에서 아이콘 표시하기

    float hp = 100.0f;
    float maxHP = 100.0f;

    public float HP
    {
        get => hp;
        private set
        {
            if (IsAlive)
            {
                hp = value;
                if( hp <= 0.0f)
                {
                    Die();
                }

                hp = Mathf.Clamp(hp, 0.0f, maxHP);
                Debug.Log($"Hp : {hp}");
            }
        }
    }
    public float MaxHP => maxHP;

    public bool IsAlive => hp > 0;

    public event Action<float> onHealthChange;

    public event Action onDie;

    public void HealthHeal(float heal)
    {
        HP += heal;
    }

    public void HealthRegenerate(float totalRegen, float duration)
    {
        StartCoroutine(RegenCoroutine(totalRegen, duration, true));
    }

    public void HealthRegenetateByTick(float tickRegen, float tickInterval, uint totalTickCount)
    {
        StartCoroutine(RegenByTick(tickRegen, tickInterval, totalTickCount, true));
    }

    IEnumerator RegenCoroutine(float totalRegen, float duration, bool isHP)
    {
        float regenPerSec = totalRegen / duration;
        float timeElapsed = 0.0f;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            if (isHP)
            {
                HP += Time.deltaTime * regenPerSec;
            }
            else
            {

            }
            yield return null;
        }
    }

    IEnumerator RegenByTick(float tickRegen, float tickInterval, uint totalTickCount, bool isHP)
    {
        WaitForSeconds wait = new WaitForSeconds(tickInterval);
        for (int i = 0; i < totalTickCount; i++)
        {
            if (isHP)
            {
                HP += tickRegen;
            }
            else
            {

            }
            yield return wait;
        }
    }

    public void Die()
    {
        Debug.Log("사망");
    }
}
