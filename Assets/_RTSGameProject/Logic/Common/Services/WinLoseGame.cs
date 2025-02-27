using _RTSGameProject.Logic.Common.Services;
using UnityEngine;

public class WinLoseGame
{
    private bool _isGameOver;
    private int _winCondition;
    private int _loseCondition;
    private WinLoseWindow _winLoseWindow;
    private UnitsRepository _unitsRepository;
    
    public WinLoseGame(WinLoseWindow winLoseGame, UnitsRepository unitsRepository, int winCondition, int loseCondition)
    {
        _winLoseWindow = winLoseGame;
        _unitsRepository = unitsRepository;
        _winCondition = winCondition;
        _loseCondition = loseCondition;
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
            _winLoseWindow.gameObject.SetActive(true);
            _winLoseWindow.WinPanel.SetActive(true);
            GameOver();
        }
    }

    private void UnitKilled()
    {
        _loseCondition -= 1;
        if (_loseCondition <= 0)
        {
            _winLoseWindow.gameObject.SetActive(true);
            _winLoseWindow.LosePanel.SetActive(true);
            GameOver();
        }
    }

    private void GameOver()
    {
        _isGameOver = true;
        Time.timeScale = 0;
    }
}
