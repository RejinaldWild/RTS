using System;
using _RTSGameProject.Logic.Ads;
using _RTSGameProject.Logic.Analytic;
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
        public event Action OnRemoveLose;
        
        private int _winCondition;
        private int _loseCondition;
        private int _quantityOfEnemiesKilled;
        private int _quantityOfUnitsCasualties;
        private WinLoseWindow _winLoseWindow;
        private WinLoseWindowProvider _winLoseWindowProvider;
        private SceneChanger _sceneChanger;
        
        private readonly UnitsRepository _unitsRepository;
        private readonly PauseGame _pauseGame;
        private readonly IAnalyticService _analyticService;
        private readonly IAdsService _adsService;
    
        public WinLoseGame(PauseGame pauseGame,
                            WinLoseWindowProvider winLoseWindowProvider,
                            UnitsRepository unitsRepository, 
                            WinLoseConfig winLoseCondition,
                            SceneChanger sceneChanger,
                            IAnalyticService analyticService,
                            IAdsService adsService)
        {
            _unitsRepository = unitsRepository;
            _analyticService = analyticService;
            _adsService = adsService;
            _sceneChanger = sceneChanger;
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
            _sceneChanger.OnRewardedAdWatch += OnRewardedAdWatched;
            _sceneChanger.OnContinueToPlay += OnContinueToPlayed;
            _sceneChanger.OnMainMenuClick += OnMainMenuClicked;
            _winLoseWindow.Subscribe();
        }

        public void Dispose()
        {
            _unitsRepository.OnUnitKill -= UnitKilled;
            _unitsRepository.OnEnemyKill -= EnemyKilled;
            _sceneChanger.OnRewardedAdWatch -= OnRewardedAdWatched;
            _sceneChanger.OnContinueToPlay -= OnContinueToPlayed;
            _sceneChanger.OnMainMenuClick -= OnMainMenuClicked;
            _winLoseWindow.Unsubscribe();
            _winLoseWindowProvider.Unload();
        }

        private void EnemyKilled()
        {
            _winCondition -= 1;
            _quantityOfEnemiesKilled++;
            _analyticService.SendKillEnemy(_quantityOfEnemiesKilled);
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
            _quantityOfUnitsCasualties++;
            if (_loseCondition <= 0)
            {
                _winLoseWindow.gameObject.SetActive(true);
                _winLoseWindow.LosePanel.SetActive(true);
                _winLoseWindow.ContinueButton.gameObject.SetActive(false);
                OnLose?.Invoke();
                _pauseGame.Pause();
            }
            _analyticService.SendKillUnit(_quantityOfUnitsCasualties);
        }
        
        private void OnRewardedAdWatched()
        {
            _adsService.ShowRewarded();
            
            if (!_adsService.RewardedFullyWatched)
            {
                _winLoseWindow.WatchAdButton.gameObject.SetActive(true);
                _winLoseWindow.ContinueButton.gameObject.SetActive(false);
            }
            else
            {
                _winLoseWindow.WatchAdButton.gameObject.SetActive(false);
                _winLoseWindow.ContinueButton.gameObject.SetActive(true);
            }
        }
        
        private void OnContinueToPlayed()
        {
            OnRemoveLose?.Invoke();
            _pauseGame.UnPause();
            _winLoseWindow.gameObject.SetActive(false);
            _winLoseWindow.LosePanel.SetActive(false);
            _quantityOfUnitsCasualties = 0;
            _loseCondition = 0;
        }

        private void OnMainMenuClicked()
        {
            _adsService.ShowInterstitial();
            GameOver();
        }
        
        private void GameOver()
        {
            _pauseGame.Pause();
        }
    }
}
