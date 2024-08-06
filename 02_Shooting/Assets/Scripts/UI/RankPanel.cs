using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankPanel : MonoBehaviour
{
    /// <summary>
    /// 패널에서 보이는 모든 랭크 라인
    /// </summary>
    RankLine[] rankLines;

    /// <summary>
    /// 랭킹 표시되는 사람 수
    /// </summary>
    const int MaxRankings = 5;

    /// <summary>
    /// 랭킹 데이터를 초기값으로 설정하는 함수
    /// </summary>
    void SetDefaultData()
    {
        /// 1st AAA 1,000,000
        /// 2nd BBB 100,000
        /// 3rd CCC 10,000
        /// 4th DDD 1,000
        /// 5th EEE 100
    }

#if UNITY_EDITOR

    public void Test_DefaultRankPanel()
    {
        SetDefaultData();
    }
#endif
}
