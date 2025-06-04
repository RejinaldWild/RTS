using System;
using _RTSGameProject.Logic.Common.Config;
using _RTSGameProject.Logic.Common.View;
using _RTSGameProject.Logic.LoadingAssets.Local;
using Zenject;

namespace _RTSGameProject.Logic.Common.Services
{
    public class WinLoseGame: IInitializable, IDisposable
    {
        public event Action OnWin;
        public event Action OnLose;
        
        private int _winCondition;
        private int _loseCondition;
        private WinLoseWindow _winLoseWindow;
        private WinLoseWindowProvider _winLoseWindowProvider;
        private readonly UnitsRepository _unitsRepository;
        private readonly PauseGame _pauseGame;
    
        public WinLoseGame(PauseGame pauseGame, WinLoseWindowProvider winLoseWindowProvider,
            UnitsRepository unitsRepository, WinLoseConfig winLoseCondition)
        {
            _unitsRepository = unitsRepository;
            _winCondition = winLoseCondition.WinConditionKillUnits;
            _loseCondition = winLoseCondition.LoseConditionKillUnits;
            _pauseGame = pauseGame;
            _winLoseWindowProvider = winLoseWindowProvider;
        }

        public async void Initialize()
        {
            _winLoseWindow = await _winLoseWindowProvider.Load();
            _unitsRepository.OnUnitKill += UnitKilled;
            _unitsRepository.OnEnemyKill += EnemyKilled;
            _winLoseWindow.Subscribe();
        }
    
        public void Dispose()
        {
            _unitsRepository.OnUnitKill -= UnitKilled;
            _unitsRepository.OnEnemyKill -= EnemyKilled;
            _winLoseWindow.Unsubscribe();
            _winLoseWindowProvider.Unload();
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
