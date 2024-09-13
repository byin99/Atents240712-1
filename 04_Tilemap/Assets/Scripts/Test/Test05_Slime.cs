using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test05_Slime : TestBase
{
    public SpriteRenderer[] spriteRenderers;
    Material[] materials;

    public bool isOutLineChange;
    public bool isInnerLineChange;
    public bool isPhaseChange;
    public bool isPhaseReverseChange;
    public bool isDissolveChange;

    private void Update()
    {
        if (isOutLineChange)
        {
            // 아웃라인의 두께가 커졌다 작아졌다를 반복한다.(두께도 0~1로 설정 가능하게 변경)
        }
        if (isInnerLineChange)
        {
            // 이너 라인의 두께가 커졌다 작아졌다를 반복한다.(두께도 0~1로 설정 가능하게 변경)
        }
        if (isPhaseChange)
        {
            // Split값이 0~1사이를 반복한다.
        }
        if(isPhaseReverseChange)
        {
            // Split값이 0~1사이를 반복한다.
        }
        if (isDissolveChange)
        {
            // fade값이 0~1사이를 반복한다.
        }
    }

    // 아웃라인, PhaseReverse, Dissolve를 하나로 합친 합친 쉐이더 그래프 만들기.(SlimeEffect)
}
