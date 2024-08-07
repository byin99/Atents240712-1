using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RankPanel : MonoBehaviour
{
    /// <summary>
    /// 패널에서 보이는 모든 랭크 라인
    /// </summary>
    RankLine[] rankLines;

    /// <summary>
    /// 랭커 이름 입력을 위한 인풋 필드
    /// </summary>
    TMP_InputField inputField;

    /// <summary>
    /// 최고 득점자 이름(1등~5등 순서로 정렬되어 있음)
    /// </summary>
    string[] rankers;

    /// <summary>
    /// 최고 득점(1등~5등 순서로 정렬되어 있음)
    /// </summary>
    int[] highRecords;

    /// <summary>
    /// null이 아니면 직전에 랭킹이 업데이트 된 인덱스
    /// </summary>
    int? updatedIndex = null;

    /// <summary>
    /// 랭킹 표시되는 사람 수
    /// </summary>
    const int MaxRankings = 5;

    private void Awake()
    {
        rankLines = GetComponentsInChildren<RankLine>();
        rankers = new string[MaxRankings];
        highRecords = new int[MaxRankings];

        inputField = GetComponentInChildren<TMP_InputField>(true);  // 비활성화 되어있는 컴포넌트를 찾으려면 파라메터를 true로 해야 한다.
        inputField.onEndEdit.AddListener(OnNameInputEnd);   // onEndEdit의 발동여부를 확인할 함수 등록(=onEndEdit 이벤트가 발동할 때 실행될 함수 추가)
    }

    /// <summary>
    /// inputField.onEndEdit 이벤트가 발동되었을 때 실행될 함수
    /// </summary>
    /// <param name="inputText">inputField에 입력이 완료되었을 때의 입력 내용</param>
    private void OnNameInputEnd(string inputText)
    {
        //Debug.Log(inputText);

        inputField.gameObject.SetActive(false);     // 인풋필드 비활성화
        if(updatedIndex != null)
        {
            rankers[updatedIndex.Value] = inputText;    // 이름 변경
            RefreshRankLines();
            updatedIndex = null;
        }
    }

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

        int score = 1000000;

        for (int i = 0; i < MaxRankings; i++)
        {
            char temp = 'A';                // 'A' == 65
            temp = (char)((byte)temp + i);  // i만큼 증가 = A에서 i만큼 떨어진 글자로 변경
            rankers[i] = $"{temp}{temp}{temp}"; // AAA ~ EEE

            highRecords[i] = score;
            score = Mathf.RoundToInt(score * 0.1f); // 한바퀴 돌 때마다 1/10로 줄이기
        }

        RefreshRankLines();
    }

    /// <summary>
    /// 패널 보이는 부분 갱신용 함수
    /// </summary>
    void RefreshRankLines()
    {
        for(int i = 0;i<MaxRankings;i++)
        {
            rankLines[i].SetData(rankers[i], highRecords[i]);   // 저장된 데이터대로 다시 표시
        }
    }

    /// <summary>
    /// 랭킹 데이터를 업데이트하는 함수
    /// </summary>
    /// <param name="score">새 점수</param>
    void UpdataRankData(int score)
    {
        for(int i=0;i<MaxRankings;i++)
        {
            if (highRecords[i] < score)
            {
                // 신기록이다.
                for(int j = MaxRankings-1; j>i; j--)            // 마지막+1에서부터 i전까지 진행
                {
                    rankers[j] = rankers[j-1];
                    highRecords[j] = highRecords[j - 1];        // 한단계 아래쪽으로 내리기                    
                }

                rankers[i] = "새 랭커";                        // 새 랭커이름과 점수 기록하기
                highRecords[i] = score;
                updatedIndex = i;                               // 업데이트된 인덱스 저장하기

                Vector3 pos = inputField.transform.position;    // 등수에 맞게 위치 이동
                pos.y = rankLines[i].transform.position.y;
                inputField.transform.position = pos;
                inputField.text = string.Empty;                 // 인풋필드 내용 비우기
                inputField.gameObject.SetActive(true);          // 인풋필드 보이게 만들기

                RefreshRankLines(); // 화면 갱신
                break;              // for문 끝내기
            }
        }
    }

#if UNITY_EDITOR

    public void Test_DefaultRankPanel()
    {
        SetDefaultData();
    }

    public void Test_UpdateRankPanel(int score)
    {
        UpdataRankData(score);
    }
#endif
}
