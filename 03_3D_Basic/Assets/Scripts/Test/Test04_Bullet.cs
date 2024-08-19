using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test04_Bullet : TestBase
{
    public GameObject simpleBulletPrefab;

    Transform fire;

    private void Start()
    {
        fire = transform.GetChild(0);
        //Time.timeScale = 0.1f;
    }

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        Instantiate(simpleBulletPrefab, fire);
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        Factory.Instance.GetBullet(fire.position, fire.eulerAngles);
    }
}
