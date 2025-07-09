using System;
using System.Collections.Generic;
using _RTSGameProject.Logic.Ads;
using _RTSGameProject.Logic.Analytic;
using _RTSGameProject.Logic.Common.Config;
using _RTSGameProject.Logic.Common.View;
using _RTSGameProject.Logic.LoadingAssets.Local;
using UnityEngine.SceneManagement;
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
        private readonly FirebaseRemoteConfigProvider _firebaseRemoteConfigProvider;
    
        public WinLoseGame(PauseGame pauseGame,
                            WinLoseWindowProvider winLoseWindowProvider,
                            UnitsRepository unitsRepository,
                            SceneChanger sceneChanger,
                            FirebaseRemoteConfigProvider firebaseRemoteConfigProvider,
                            IAnalyticService analyticService,
                            IAdsService adsService)
        {
            _unitsRepository = unitsRepository;
            _analyticService = analyticService;
            _adsService = adsService;
            _sceneChanger = sceneChanger;
            _firebaseRemoteConfigProvider = firebaseRemoteConfigProvider;
            _pauseGame = pauseGame;
            _winLoseWindowProvider = winLoseWindowProvider;
        }

        public async void Initialize()
        {
            _winLoseWindow = await _winLoseWindowProvider.Load();
            
            foreach (KeyValuePair<string,LevelConfig> level in _firebaseRemoteConfigProvider.WinLoseConfig.Levels)
            {
                if (level.Key == $"Level{SceneManager.GetActiveScene().buildIndex}")
                {
                    LevelConfig levelConfig = level.Value;
                    _winCondition = levelConfig.WinCondition;
                    _loseCondition = levelConfig.LoseCondition;
                    break;
                }
            }
            
            _unitsRepository.OnUnitKill += UnitKilled;
            _unitsRepository.OnEnemyKill += EnemyKilled;
            _sceneChanger.OnWatchAdReward += OnWatchAdRewarded;
            _sceneChanger.OnContinueToPlay += OnContinueToPlayed;
            _sceneChanger.OnMainMenuClick += OnMainMenuClicked;
            _winLoseWindow.Subscribe();
        }

        public void Dispose()
        {
            _unitsRepository.OnUnitKill -= UnitKilled;
            _unitsRepository.OnEnemyKill -= EnemyKilled;
            _sceneChanger.OnWatchAdReward -= OnWatchAdRewarded;
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
                _winLoseWindow.LosePanel.SetActive(false);
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
                _winLoseWindow.WinPanel.SetActive(false);
                _winLoseWindow.ContinueButton.gameObject.SetActive(false);
                OnLose?.Invoke();
                _pauseGame.Pause();
            }
            _analyticService.SendKillUnit(_quantityOfUnitsCasualties);
        }
        
        private void OnWatchAdRewarded()
        {
            _adsService.ShowRewarded(() =>
            {
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
            });
            _adsService.LoadRewarded();
        }
        
        private void OnContinueToPlayed()
        {
            OnRemoveLose?.Invoke();
            _pauseGame.UnPause(); // back colours
            _winLoseWindow.gameObject.SetActive(false);
            _winLoseWindow.LosePanel.SetActive(false);
            _quantityOfUnitsCasualties = 0;
            _loseCondition = 0;
        }

        private void OnMainMenuClicked()
        {
            _adsService.ShowInterstitial();
            _adsService.LoadInterstitial();
            GameOver();
        }
        
        private void GameOver()
        {
            _pauseGame.Pause();
        }
    }
}
