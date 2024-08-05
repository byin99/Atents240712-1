using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test19_GameOver : TestBase
{
    public int score = 100;

    Player player;
    ScoreText scoreText;

    private void Start()
    {
        player = GameManager.Instance.Player;
        scoreText = GameManager.Instance.ScoreText;
    }

#if UNITY_EDITOR

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        player.TestDeath();
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        scoreText.AddScore(score);
    }
#endif
}
