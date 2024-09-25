using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsyncLoadingBackground : MonoBehaviour
{
    public string nextSceneName = "LoadSampleScene";

    public float tickTime = 0.2f;

    /// <summary>
    /// slider의 value가 증가하는 속도
    /// </summary>
    public float loadingBarSpeed = 1.0f;


    // 1. LoadingText에 표시되는 글자가
    //    "Loading", "Loading .", "Loading . .", "Loading . . .", "Loading . . . .", "Loading . . . . ."가
    //    tickTime마다 변경되면서 반복된다.
    // 2. slider의 value가 loadingBarSpeed의 속도로 증가한다.
    // 3. slider의 value가 1이 되기 전에 로딩이 완료되면 slider의 value가 1이 될 때까지 기다린다.
    // 4. 로딩이 완료되면 LoadingText에 표시되는 글자가 "Loading Complete!"로 변경된다.
    // 5. 로딩이 완료된 이후에 마우스나 키보드 중 아무키나 눌려지면 씬 전환이 일어난다.
}
