using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test19_GameOver : TestBase
{
    public int score = 100;
    public bool isStartDie = false;

    Player player;
    ScoreText scoreText;

#if UNITY_EDITOR

    private void Start()
    {
        player = GameManager.Instance.Player;
        scoreText = GameManager.Instance.ScoreText;

        if(isStartDie)
        {
            player.TestDeath();
        }
    }


    protected override void OnTest1(InputAction.CallbackContext context)
    {
        player.TestDeath();
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        scoreText.AddScore(score);
    }

    protected override void OnTest3(InputAction.CallbackContext context)
    {
        RankLine line = FindFirstObjectByType<RankLine>();
        line.SetData("가가가", score);
    }

    protected override void OnTest4(InputAction.CallbackContext context)
    {
        RankPanel panel = FindFirstObjectByType<RankPanel>();
        panel.Test_DefaultRankPanel();
    }
#endif
}
