using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScreen : Screen
{
    [SerializeField] Text health;
    [SerializeField] Text score;
    [SerializeField] GameObject immune;
    public void SetScore(int _score)
    {
        score.text = $"{_score}";
    }

    public void SetHealth(int _health, bool activate_immune)
    {
        health.text = $"{_health}";
        if (activate_immune)
            ImmuneActivate();
    }

    public void ImmuneActivate()
    {
        immune.SetActive(true);
        Invoke("ImmuneDeactivate", GameData.ImmuneTime);
    }

    void ImmuneDeactivate()
    {
        immune.SetActive(false);
    }
    protected override void SelfClose()
    {
        
    }

    protected override void SelfOpen()
    {
        
    }
}
