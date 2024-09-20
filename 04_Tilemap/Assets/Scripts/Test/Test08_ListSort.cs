using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test08_ListSort : TestBase
{
    protected override void OnTest1(InputAction.CallbackContext context)
    {
        List<int> list = new List<int>() { 1, 10, 5, 7, 3 };
        Debug.Log("정렬 전");
        foreach (int i in list)
        {
            Debug.Log(i);
        }
        list.Sort();
        Debug.Log("정렬 후");
        foreach (int i in list)
        {
            Debug.Log(i);
        }
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        List<Node> list = new List<Node>();
        Node temp = new Node(0, 0);
        temp.G = 10;
        temp.H = 30;
        list.Add( temp );

        temp = new Node(1, 0);
        temp.G = 20;
        temp.H = 40;
        list.Add(temp);

        temp = new Node(4, 0);
        temp.G = 50;
        temp.H = 50;
        list.Add(temp);

        temp = new Node(3, 0);
        temp.G = 40;
        temp.H = 10;
        list.Add(temp);

        temp = new Node(2, 0);
        temp.G = 30;
        temp.H = 20;
        list.Add(temp);

        Debug.Log("정렬 전");
        foreach (Node node in list)
        {
            Debug.Log($"{node.G}, {node.H}");
        }
        list.Sort();    // Node의 CompareTo 함수에 따라 정렬
        //list.Sort((x,y) => x.H.CompareTo(y.H));   // 내가 원하는 방법을 기록해둔 람다 함수를 이용해 정렬
        Debug.Log("정렬 후");
        foreach (Node node in list)
        {
            Debug.Log($"{node.G}, {node.H}");
        }
    }

    protected override void OnTest3(InputAction.CallbackContext context)
    {
        //int i = 10;
        //int j = 20;
        //Debug.Log(i.CompareTo(j));

        //j = 10;
        //Debug.Log(i.CompareTo(j));

        //j = 5;
        //Debug.Log(i.CompareTo(j));

        //float a;
        //string b;


        Node a = new Node(1, 0);
        Node b = new Node(1, 0);
        Node c = a;

        Debug.Log(a == b);
        Debug.Log(a == c);

        Vector2Int temp = new Vector2Int(1,0);
        Debug.Log(a == temp);

    }
}
