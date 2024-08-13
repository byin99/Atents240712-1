using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestGenerator : MonoBehaviour
{
    // 여러개의 나무 프리팹을 가진다.
    // forestGenerate가 true가 되면
    // generateCenter위치가 중심이고 가로가 width 세로가 height인 사각형 영역안의 랜덤한 위치에
    // treeCount만큼의 Tree1 타입의 나무를 생성한다.(생성된 나무는 Randomize 실행)
    // 생성 영역은 Gizmos로 표시한다.

    public bool forestGenerate = false;

    public GameObject[] treePrefabs;

    public enum TreeType
    {
        Tree1,
        Tree2
    }

    public TreeType type = TreeType.Tree1;
    
    public float width = 10.0f;
    public float height = 10.0f;
    Transform generateCenter;

    public int treeCount = 10;

}
