using System;
using _RTSGameProject.Logic.Common.Config;
using _RTSGameProject.Logic.Common.Services;
using _RTSGameProject.Logic.Common.View;
using UnityEngine;

public class WinLoseGame
{
    public Action OnWin;
    public Action OnLose;
    
    private bool _isGameOver;
    private int _winCondition;
    private int _loseCondition;
    private WinLoseWindow _winLoseWindow;
    private UnitsRepository _unitsRepository;
    private PauseGame _pauseGame;
    
    public WinLoseGame(WinLoseWindow winLoseWindow, PauseGame pauseGame, 
                        UnitsRepository unitsRepository, WinLoseConfig winLoseCondition)
    {
        _isGameOver = false;
        _winLoseWindow = winLoseWindow;
        _unitsRepository = unitsRepository;
        _winCondition = winLoseCondition.WinConditionKillUnits;
        _loseCondition = winLoseCondition.LoseConditionKillUnits;
        _pauseGame = pauseGame;
    }

    public void Subscribe()
    {
        _unitsRepository.OnUnitKill += UnitKilled;
        _unitsRepository.OnEnemyKill += EnemyKilled;
    }
    
    public void Unsubscribe()
    {
        _unitsRepository.OnUnitKill -= UnitKilled;
        _unitsRepository.OnEnemyKill -= EnemyKilled;
    }

    private void EnemyKilled()
    {
        _winCondition -= 1;
        if (_winCondition <= 0)
        {
            OnWin?.Invoke();
            _winLoseWindow.gameObject.SetActive(true);
            _winLoseWindow.WinPanel.SetActive(true);
            GameOver();
            _isGameOver = false;
        }
    }

    private void UnitKilled()
    {
        _loseCondition -= 1;
        if (_loseCondition <= 0)
        {
            OnLose?.Invoke();
            _winLoseWindow.gameObject.SetActive(true);
            _winLoseWindow.LosePanel.SetActive(true);
            GameOver();
            _isGameOver = false;
        }
    }

    private void GameOver()
    {
        _isGameOver = true;
        _pauseGame.Pause();
    }
}
