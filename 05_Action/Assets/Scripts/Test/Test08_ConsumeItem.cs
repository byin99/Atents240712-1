using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test08_ConsumeItem : Test07_ItemPickUpAndDrop
{
    PlayerStatus status;

    protected override void Awake()
    {
        base.Awake();
        code = ItemCode.SilverCoin;
    }

    protected override void Start()
    {
        base.Start();

        status = player.GetComponent<PlayerStatus>();
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        status.HealthHeal(-90);
    }
}
