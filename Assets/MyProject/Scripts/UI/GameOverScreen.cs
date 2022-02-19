using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : Screen
{
    [SerializeField] Button restartBtn;
    [SerializeField] Text bestScore;

    protected override void Initialization()
    {
        base.Initialization();
        restartBtn.onClick.AddListener(RestartClick);
    }
    void RestartClick()
    {
        GameController.Instance.Reset();
        UIController.Instance.GameScreen();
    }

    protected override void SelfClose()
    {
        panel.alpha = 0;
    }

    protected override void SelfOpen()
    {
        panel.alpha = 1;

        var best = PlayerPrefs.GetInt("BestScore", 0);
        if (GameController.Instance.Score > best)
        {
            best = GameController.Instance.Score;
            PlayerPrefs.SetInt("BestScore", best);
        }

        bestScore.text = $"{best}";
    }
}
