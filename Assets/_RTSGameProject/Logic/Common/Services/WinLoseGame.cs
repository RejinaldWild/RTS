using System;
using _RTSGameProject.Logic.Common.Config;
using _RTSGameProject.Logic.Common.View;
using Zenject;

namespace _RTSGameProject.Logic.Common.Services
{
    public class WinLoseGame: IInitializable, IDisposable
    {
        public event Action OnWin;
        public event Action OnLose;

        private int _winCondition;
        private int _loseCondition;
        private readonly WinLoseWindow _winLoseWindow;
        private readonly UnitsRepository _unitsRepository;
        private readonly PauseGame _pauseGame;
    
        public WinLoseGame(WinLoseWindow winLoseWindow, PauseGame pauseGame, 
            UnitsRepository unitsRepository, WinLoseConfig winLoseCondition)
        {
            _winLoseWindow = winLoseWindow;
            _unitsRepository = unitsRepository;
            _winCondition = winLoseCondition.WinConditionKillUnits;
            _loseCondition = winLoseCondition.LoseConditionKillUnits;
            _pauseGame = pauseGame;
        }

        public void Initialize()
        {
            _unitsRepository.OnUnitKill += UnitKilled;
            _unitsRepository.OnEnemyKill += EnemyKilled;
            _winLoseWindow.Subscribe();
        }
    
        public void Dispose()
        {
            _unitsRepository.OnUnitKill -= UnitKilled;
            _unitsRepository.OnEnemyKill -= EnemyKilled;
            _winLoseWindow.Unsubscribe();
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
            }
        }

        private void GameOver()
        {
            _pauseGame.Pause();
        }
    }
}
