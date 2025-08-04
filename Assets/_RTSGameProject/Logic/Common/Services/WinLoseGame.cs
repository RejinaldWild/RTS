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
        
        private readonly WinLoseWindowProvider _winLoseWindowProvider;
        private readonly UnitsRepository _unitsRepository;
        private readonly PauseGame _pauseGame;
        private readonly ISceneChanger _sceneChanger;
        private readonly IAnalyticService _analyticService;
        private readonly IAdsService _adsService;
        private readonly IRemoteConfigProvider _remoteConfigProvider;
        
        public WinLoseWindow WinLoseWindowProp { get; private set; }
    
        public WinLoseGame(PauseGame pauseGame,
                            WinLoseWindowProvider winLoseWindowProvider,
                            UnitsRepository unitsRepository,
                            ISceneChanger sceneChanger,
                            IRemoteConfigProvider remoteConfigProvider,
                            IAnalyticService analyticService,
                            IAdsService adsService)
        {
            _unitsRepository = unitsRepository;
            _sceneChanger = sceneChanger;
            _analyticService = analyticService;
            _adsService = adsService;
            _remoteConfigProvider = remoteConfigProvider;
            _pauseGame = pauseGame;
            _winLoseWindowProvider = winLoseWindowProvider;
        }

        public async void Initialize()
        {
            WinLoseWindowProp = await _winLoseWindowProvider.Load();
            var winLoseActions = new WinLoseActions(this);
            WinLoseWindowProp.Construct(winLoseActions);
            
            foreach (KeyValuePair<string,LevelConfig> level in _remoteConfigProvider.WinLoseConfig.Levels)
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
            WinLoseWindowProp.Subscribe();
        }

        public void Dispose()
        {
            _unitsRepository.OnUnitKill -= UnitKilled;
            _unitsRepository.OnEnemyKill -= EnemyKilled;
            WinLoseWindowProp.Unsubscribe();
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
                WinLoseWindowProp.gameObject.SetActive(true);
                WinLoseWindowProp.WinPanel.SetActive(true);
                WinLoseWindowProp.LosePanel.SetActive(false);
                GameOver();
            }
        }

        private void UnitKilled()
        {
            _loseCondition -= 1;
            _quantityOfUnitsCasualties++;
            if (_loseCondition <= 0)
            {
                WinLoseWindowProp.gameObject.SetActive(true);
                WinLoseWindowProp.LosePanel.SetActive(true);
                WinLoseWindowProp.WinPanel.SetActive(false);
                WinLoseWindowProp.ContinueButton.gameObject.SetActive(false);
                OnLose?.Invoke();
                _pauseGame.Pause();
            }
            _analyticService.SendKillUnit(_quantityOfUnitsCasualties);
        }
        
        public void ToWatchRewardAd()
        {
            _adsService.ShowRewarded(() =>
            {
                if (!_adsService.RewardedFullyWatched)
                {
                    WinLoseWindowProp.WatchAdButton.gameObject.SetActive(true);
                    WinLoseWindowProp.ContinueButton.gameObject.SetActive(false);
                }
                else
                {
                    WinLoseWindowProp.WatchAdButton.gameObject.SetActive(false);
                    WinLoseWindowProp.ContinueButton.gameObject.SetActive(true);
                }
            });
            _adsService.LoadRewarded();
        }
        
        public void ToContinueToPlay()
        {
            OnRemoveLose?.Invoke();
            WinLoseWindowProp.gameObject.SetActive(false);
            WinLoseWindowProp.LosePanel.SetActive(false);
            _pauseGame.UnPause();
            _quantityOfUnitsCasualties = 0;
            _loseCondition = 0;
        }
        
        private void GameOver()
        {
            _pauseGame.Pause();
        }

        public void ToNextLevel()
        {
            _sceneChanger.ToNextLevel();
        }

        public void ToMainMenu()
        {
            if (!_adsService.IsPaidForRemovingAds)
            {
                _adsService.ShowInterstitial();
                _adsService.LoadInterstitial();
            }
            GameOver();
            _sceneChanger.ToMainMenu();
        }
    }
}
