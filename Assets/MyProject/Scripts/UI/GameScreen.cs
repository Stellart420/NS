using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScreen : Screen
{
    [SerializeField] Text health;
    [SerializeField] Text score;
    [SerializeField] GameObject immune;
    [SerializeField] Text enemyCount;
    [SerializeField] Text closestEnemy;
    [SerializeField] Text crystalCount;
    [SerializeField] Text closestCrystal;

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

    public void SetClosestEnemyDistance(float _distance)
    {
        closestEnemy.text = $"{Math.Round(_distance,1)}";
    }

    public void SetClosestCrystalDistance(float _distance)
    {
        closestCrystal.text = $"{Math.Round(_distance, 1)}";
    }

    public void SetEnemies(int _count)
    {
        enemyCount.text = $"{_count}";
    }

    public void SetCrystals(int _count)
    {
        crystalCount.text = $"{_count}";
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
