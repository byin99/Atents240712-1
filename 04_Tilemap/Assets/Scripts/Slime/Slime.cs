using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : RecycleObject
{
    /// <summary>
    /// 슬라임의 이동 속도
    /// </summary>
    public float moveSpeed = 2.0f;

    /// <summary>
    /// 페이즈 진행 시간(등장 연출 시간)
    /// </summary>
    public float phaseDuration = 0.5f;

    /// <summary>
    /// 디졸브 진행시간(사망 연출 시간)
    /// </summary>
    public float dissolveDuration = 1.0f;

    /// <summary>
    /// 아웃라인 기본 두깨
    /// </summary>
    public float VisibleOutlineThickness = 0.5f;

    /// <summary>
    /// 페이즈의 기본 두깨
    /// </summary>
    public float VisiblePhaseThickness = 0.1f;

    /// <summary>
    /// 슬라임이 움직일 그리드 맵
    /// </summary>
    TileGridMap map;

    /// <summary>
    /// 슬라임이 따라 움직일 경로
    /// </summary>
    List<Vector2Int> path;

    /// <summary>
    /// 슬라임이 이동할 경로를 그려주는 객체
    /// </summary>
    PathLine pathLine;

    /// <summary>
    /// 이 슬라임이 위치하고 있는 노드
    /// </summary>
    Node current = null;

    /// <summary>
    /// 경로를 보여줄지 말지 결정하는 변수
    /// </summary>
    bool isShowPath = false;

    /// <summary>
    /// 슬라임의 이동이 활성화 되었는지 여부(true면 움직임, false면 안움직임)
    /// </summary>
    bool isMoveActivate = false;

    /// <summary>
    /// 머티리얼
    /// </summary>
    Material mainMaterial;

    /// <summary>
    /// 현재 슬라임의 위치를 그리드 좌표로 알려주는 프로퍼티
    /// </summary>
    Vector2Int GridPosition => map.WorldToGrid(transform.position);

    /// <summary>
    /// 슬라임이 위치한 노드 확인 및 변경용 프로퍼티
    /// </summary>
    Node Current
    {
        get => current;
        set
        {
            if (current != value)       // current에 변화가 있을 때
            {
                if( current != null )   // 기존 current가 null 아니었다면
                {
                    current.nodeType = Node.NodeType.Plain; // 노드의 타입을 plain으로 되돌리기
                }
                current = value;
                if (current != null)    // 새 current가 null이 아니라면
                {
                    current.nodeType = Node.NodeType.Slime; // 노드의 타입을 Slime으로 변경하기
                }
            }
        }
    }

    // 쉐이더 프로퍼티용 ID들
    readonly int OutlineThicknessID = Shader.PropertyToID("_OutlineThickness");
    readonly int PhaseSplitID = Shader.PropertyToID("_PhaseSplit");
    readonly int PhaseThicknessID = Shader.PropertyToID("_PhaseThickness");
    readonly int DissolveFadeID = Shader.PropertyToID("_DissolveFade");


    // 슬라임은 풀로 관리된다. 팩토리를 이용해 생성할 수 있다.

    private void Awake()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        mainMaterial = spriteRenderer.material;

        path = new List<Vector2Int>();
        pathLine = GetComponentInChildren<PathLine>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        // 스폰될 때 Phase 작동
        StartCoroutine(StartPhase());
    }

    protected override void OnReset()
    {
        ShowOutline(false);     // 아웃라인 끄기
        mainMaterial.SetFloat(PhaseThicknessID, VisiblePhaseThickness); // 페이즈 두깨 원상복구 시키기
        mainMaterial.SetFloat(PhaseSplitID, 1);         // 페이즈 시작 값으로 설정
        mainMaterial.SetFloat(DissolveFadeID, 1);       // 디졸브 시작 값으로 설정

        isMoveActivate = false;     // 이동 비활성화
    }

    protected override void OnDisable()
    {
        // ReturnToPool()에서 할 일을 여기로
        
        path.Clear();
        pathLine.ClearPath();

        base.OnDisable();
    }

    private void Update()
    {
        MoveUpdate();
    }

    /// <summary>
    /// 슬라임 초기화 함수(스폰 직후에 실행해야 함)
    /// </summary>
    /// <param name="map">슬라임이 존재할 타일그리드맵</param>
    /// <param name="worldPos">슬라임의 시작위치(월드좌표)</param>
    public void Initialize(TileGridMap map, Vector3 worldPos)
    {
        this.map = map;         // 맵 저장
        transform.position = map.GridToWorld(map.WorldToGrid(worldPos));    // worldPos가 있는 셀의 가운데 위치에 배치
        Current = map.GetNode(worldPos);
    }

    /// <summary>
    /// Update에서 이동 처리하는 함수
    /// </summary>
    private void MoveUpdate()
    {
        if(isMoveActivate)  //isMoveActivate가 활성화 되어 있을 때만 처리
        {
            // path가 설정되어 있으면 path를 따라 계속 이동한다.
            if ( path != null && path.Count > 0 )
            {
                Vector2Int destGrid = path[0];
            
                Vector3 destPos = map.GridToWorld(destGrid);
                Vector3 direction = destPos - transform.position;

                if( direction.sqrMagnitude < 0.001f )
                {
                    // 도착했다.
                    transform.position = destPos;
                    path.RemoveAt(0);
                }
                else
                {
                    transform.Translate(Time.deltaTime * moveSpeed * direction.normalized);
                    Current = map.GetNode(transform.position);
                }
            }
            else
            {
                // 목적지에 도착했다.
                SetDestination(map.GetRandomMovablePostion());
            }
        }
    }

    IEnumerator StartPhase()
    {
        // PhaseSplitID를 1 -> 0으로 만들기

        float phaseNormalize = 1.0f / phaseDuration;    // 나누기 연산을 줄이기 위해 미리 계산
        float timeElapsed = 0.0f;                       // 시간 누적용

        while (timeElapsed < phaseDuration)             // 시간 될때까지 반복
        {
            timeElapsed += Time.deltaTime;              // 시간 누적
            
            //mainMaterial.SetFloat(PhaseSplitID, 1 - (timeElapsed/phaseDuration));
            mainMaterial.SetFloat(PhaseSplitID, 1 - (timeElapsed * phaseNormalize));    // split값을 1 -> 0으로 점점 감소시키기
            
            yield return null;
        }

        mainMaterial.SetFloat(PhaseThicknessID, 0);     // 페이즈 선 안보이게 만들기
        mainMaterial.SetFloat(PhaseSplitID, 0);         // 숫자 0으로 정리하기

        isMoveActivate = true;                          // 움직이기 시작
    }

    public void Die()
    {
        isMoveActivate = false;             // 죽으면 이동중지
        StartCoroutine(StartDissolve());    // 죽을 때 Dissolve 작동
    }

    IEnumerator StartDissolve()
    {
        // DissolveFadeID를 1 -> 0으로 만들기

        float fadeNormalize = 1.0f / dissolveDuration;  // 나누기 연산을 줄이기 위해 미리 계산
        float timeElapsed = 0.0f;                       // 시간 누적용

        while (timeElapsed < dissolveDuration)          // 시간 될때까지 반복
        {
            timeElapsed += Time.deltaTime;              // 시간 누적

            //mainMaterial.SetFloat(DissolveFadeID, 1 - (timeElapsed/dissolveDuration));
            mainMaterial.SetFloat(DissolveFadeID, 1 - (timeElapsed * fadeNormalize));    // fade값을 1 -> 0으로 점점 감소시키기

            yield return null;
        }

        mainMaterial.SetFloat(DissolveFadeID, 0);       // 숫자 0으로 정리하기
        
        gameObject.SetActive(false);                    // 게임 오브젝트 비활성화
    }

    /// <summary>
    /// 아웃라인을 보여줄지 말지 결정하는 함수
    /// </summary>
    /// <param name="isShow">true면 보여주고 false면 안보여준다.</param>
    public void ShowOutline(bool isShow = true)
    {
        // isShow가 true면 VisibleOutlineThickness, 아니면 0으로 세팅
        mainMaterial.SetFloat(OutlineThicknessID, isShow ? VisibleOutlineThickness : 0);  
    }

    /// <summary>
    /// 슬라임의 목적지를 지정하는 함수
    /// </summary>
    /// <param name="destination">목적지(그리드 좌표)</param>
    public void SetDestination(Vector2Int destination)
    {
        path = AStar.PathFind(map, GridPosition, destination);  // 경로 찾기
        if(isShowPath)
        {
            pathLine.DrawPath(map, path);   // 경로 그려주기
        }
    }

    /// <summary>
    /// 슬라임의 목적지를 지정하는 함수
    /// </summary>
    /// <param name="destination">목적지(월드좌표)</param>
    public void SetDestination(Vector3 destination)
    {
        Vector2Int grid = map.WorldToGrid(destination);
        if (map.IsValidPosition(grid) && map.IsPlain(grid)) // 지정 가능한 위치인지 확인
        {
            SetDestination(grid);
        }
    }

    /// <summary>
    /// 경로를 보여줄지 말지 결정하는 함수
    /// </summary>
    /// <param name="isShow">true면 보여주고, false면 보여주지 않는다.</param>
    public void ShowPath(bool isShow = true)
    {
        isShowPath = isShow;
        if (isShowPath)
        {
            pathLine.DrawPath(map, path);
        }
        else
        {
            pathLine.ClearPath();
        }
}
}
