using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test08_ConsumeItem : Test07_ItemPickUpAndDrop
{
    protected override void Awake()
    {
        base.Awake();
        code = ItemCode.SilverCoin;
    }
}
