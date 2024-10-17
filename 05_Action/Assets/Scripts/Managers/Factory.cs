using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : Singleton<Factory>
{
    ItemPool itemPool;

    protected override void OnInitialize()
    {
        Transform child = transform.GetChild(0);
        itemPool = child.GetComponent<ItemPool>();
        itemPool?.Initialize();
    }
}
