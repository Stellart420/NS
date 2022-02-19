using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreen : Screen
{
    [SerializeField] Button playBtn;

    protected override void Initialization()
    {
        playBtn.onClick.AddListener(PlayClick);
    }

    void PlayClick()
    {
        UIController.Instance.GameScreen();
    }

    protected override void SelfClose()
    {
        panel.alpha = 0;
    }

    protected override void SelfOpen()
    {
        panel.alpha = 1;
    }
}
