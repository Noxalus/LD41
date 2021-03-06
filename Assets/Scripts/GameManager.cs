﻿using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public EnemySpawner enemySpawner;
    public SoundManager soundManager;
    public int initialLife;
    public int initialMoney;
    public Text moneyLabel;
    public Text lifeLabel;
    public GameObject gameOverUI;
    public int towerCost;
    public int beatMissWaste;
    public Text TimeLabel;

    private int _life;
    private int _money;
    private bool _gameIsOver;
    private TimeSpan _initialTime;

    void Start()
    {
        _life = initialLife;
        _money = initialMoney;
        _gameIsOver = false;

        UpdateMoneyLabel();
        UpdateLifeLabel();

        gameOverUI.SetActive(false);

        _initialTime = TimeSpan.FromSeconds(Time.time);
    }

    private void Update()
    {
        if (_gameIsOver && Input.GetMouseButtonDown(0))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        if (!_gameIsOver)
        {
            TimeSpan currentTime = TimeSpan.FromSeconds(Time.time) - _initialTime;
            TimeLabel.text = currentTime.Minutes.ToString("00") + ":" + currentTime.Seconds.ToString("00");
        }
    }

    private void UpdateMoneyLabel()
    {
        moneyLabel.text = _money.ToString();
    }

    private void UpdateLifeLabel()
    {
        lifeLabel.text = _life.ToString();
    }

    public bool CanPlaceTower()
    {
        return _money >= towerCost;
    }

    public void OnBeatMissed()
    {
        if (_gameIsOver)
            return;

        // TODO: Play an animation
        soundManager.PlaySound("beatMissedSound");
        WasteMoney(beatMissWaste);
    }

    public void OnBeatHit()
    {
        // TODO: TowerManager trigger towers' shoot
    }

    private void WasteMoney(int waste)
    {
        _money = Mathf.Max(0, _money - waste);
        UpdateMoneyLabel();
    }

    public void OnEnemyDeath(EnemyAction enemyAction)
    {
        if (_gameIsOver)
            return;

        soundManager.PlaySound("enemyDeathSound");

        _money += enemyAction.enemy.moneyDropped;
        UpdateMoneyLabel();

        soundManager.PlaySound("moneyEarnedSound");

    }

    public void OnEnemyExit(EnemyAction enemyAction)
    {
        if (_gameIsOver)
            return;

        _life--;
        UpdateLifeLabel();

        soundManager.PlaySound("enemyExitSound");

        if (_life == 0)
            GameOver();
    }

    public void OnTowerAdded()
    {
        if (_gameIsOver)
            return;

        WasteMoney(towerCost);
        soundManager.PlaySound("towerPlacedSound");
    }

    private void GameOver()
    {
        gameOverUI.SetActive(true);
        _gameIsOver = true;

        soundManager.PlaySound("gameOverSound");
    }
}
