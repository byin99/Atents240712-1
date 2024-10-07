//#define PrintTestLog

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test05_Inventory : TestBase
{
    private void Start()
    {
        //Inventory inventory = new Inventory();
        //InvenSlot slot = inventory.slots[0];
        //InvenSlot slot = inventory[0];

        uint i = 0;
        i--;
        Debug.Log(i);

        //InvenSlot slot = new InvenSlot();
        //if(slot.ItemData != null )
        //{
        //}

        //if(!slot.IsEmpty)
        //{
        //}

#if PrintTestLog
        Debug.Log("Test");
#endif

        InvenTempSlot tempSlot = new InvenTempSlot();
        tempSlot.FromIndex = 10;
        int test = 0;

        ItemData data = GameManager.Instance.ItemData[ItemCode.Misc];
    }
}
