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
    }

    protected override void SelfOpen()
    {
    }
}
