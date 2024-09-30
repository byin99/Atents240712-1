using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Experimental.Rendering.Universal;

public class LightManager : MonoBehaviour
{
    Light2D light2D;

    private void Awake()
    {
        light2D = GetComponent<Light2D>();
    }

    private void Start()
    {
        Player player = GameManager.Instance.Player;
        if (player != null)
        {
            player.onDie += () =>
            {
                // 런타임에 특정 타입의 맴버에 접근할 수 있는 방법
                System.Reflection.FieldInfo field = typeof(Light2D).GetField(   // 특정 필드(변수) 찾기
                    "m_ApplyToSortingLayers",                                   // 찾을 이름
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);    // 찾을 때의 옵션

                int[] sortingLayers = new int[]
                {
                    SortingLayer.NameToID("Player")
                };

                field.SetValue(light2D, sortingLayers); // 찾은 필드에 값 설정
            };
        }

        

    }
}
