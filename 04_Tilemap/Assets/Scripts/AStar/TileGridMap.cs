using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileGridMap : GridMap
{
    /// <summary>
    /// 맵의 원점
    /// </summary>
    Vector2Int origin;

    /// <summary>
    /// 배경 타일맵(크기 확인 및 좌표계산용으로 사용)
    /// </summary>
    Tilemap background;

    /// <summary>
    /// 이동 가능한 지역(평지)의 모음
    /// </summary>
    Vector2Int[] movablePositions;

    /// <summary>
    /// 타일맵을 이용해 그리드맵을 생성하는 생성자
    /// </summary>
    /// <param name="background">배경용 타일맵(가로, 세로, 맵 위치 등등)</param>
    /// <param name="obstacle">장애물용 타일맵(길찾기 할 때 못가는 지역)</param>
    public TileGridMap(Tilemap background, Tilemap obstacle)
    {
        this.background = background;           // background 저장

        width = background.size.x;              // 가로/세로 길이 설정
        height = background.size.y;

        origin = (Vector2Int)background.origin; // 원점 저장

        nodes = new Node[width * height];       // 노드 배열 만들기

        Vector2Int min = (Vector2Int)background.cellBounds.min;     // for를 위한 최소/최대 값. 코드가 길어지는 것을 방지하기 위한 용도
        Vector2Int max = (Vector2Int)background.cellBounds.max;
        List<Vector2Int> movable = new List<Vector2Int>(width * height);    // 이동 가능한 지역을 임시로 기록할 리스트

        for (int y = min.y; y < max.y; y++)         // background 위치와 크기에 맞춰서 그리드 순회
        {
            for (int x = min.x; x < max.x; x++)
            {
                Node.NodeType nodeType = Node.NodeType.Plain;   // 기본 값(tile이 없으면 평지)
                TileBase tile = obstacle.GetTile(new(x, y));    // 장애물 타일맵에서 타일 가져오기 시도
                if (tile != null)
                {
                    nodeType = Node.NodeType.Wall;              // 있으면 그곳은 못가는 지역
                }
                else
                {
                    movable.Add(new Vector2Int(x, y));          // 없으면 갈 수 있는 지역이다.
                }

                nodes[CalcIndex(x, y)] = new Node(x, y, nodeType);  // 인덱스 위치에 노드 생성
            }
        }

        movablePositions = movable.ToArray();   // 임시 리스트를 배열로 변경해서 저장
    }

    protected override int CalcIndex(int x, int y)
    {
        // x + y * width;
        return base.CalcIndex(x, y);
    }

    public override bool IsValidPosition(int x, int y)
    {
        // x < width && y < height && x > -1 && y > -1
        return base.IsValidPosition(x, y);
    }
}
