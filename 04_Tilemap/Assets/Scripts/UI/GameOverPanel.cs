using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
    public float alphaChangeSpeed = 1.0f;

    // 1. 플레이어가 죽으면 alphaChangeSpeed 속도에 따라 캔버스 그룹의 알파값이 증가한다.
    // 2. 플레이어가 죽으면 플레이어의 전체 플레이 시간과 킬 수를 UI에서 갱신해야 한다.
    // 3. 플레이어가 죽은 이후에만 버튼이 눌러져야 한다.
    // 4. 버튼을 누르면 모든 씬이 Unload된 이후에 LoadingScene을 로딩한다.
}
