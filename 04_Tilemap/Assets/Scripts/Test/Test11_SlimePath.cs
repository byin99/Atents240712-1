using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class Test11_SlimePath : TestBase
{
    public Tilemap background;
    public Tilemap obstacle;
    public Slime slime;

    TileGridMap map;

    private void Start()
    {
        map = new TileGridMap(background, obstacle);
        slime.Initialize(map, slime.transform.position);
        slime.ShowPath();
    }

    protected override void OnTestLClick(InputAction.CallbackContext context)
    {
        // 클릭한 위치로 이동하기
        Vector2 screen = Mouse.current.position.ReadValue();
        Vector3 world = Camera.main.ScreenToWorldPoint(screen);
        slime.SetDestination(world);
    }

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        // 랜덤한 위치 하나 골라서 이동하기
        slime.SetDestination(map.GetRandomMovablePostion());
    }
}
