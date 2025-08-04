using System;
using _RTSGameProject.Logic.Analytic;
using _RTSGameProject.Logic.Common.SaveLoad;
using _RTSGameProject.Logic.Common.Score.Model;
using _RTSGameProject.Logic.Common.Score.View;
using _RTSGameProject.Logic.LoadingAssets.Local;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using Zenject;

namespace _RTSGameProject.Logic.Common.Services
{
    public class ScoreGameController: IDisposable, ITickable
    {
        private readonly WinLoseGame _winLoseGame;
        private readonly ScoreGameUIProvider _scoreGameUIProvider;
        private readonly IAnalyticService _analyticService;
        private readonly ISaveService _saveService;
        
        private ScoreGameUI _scoreGameUI;
        private string _key;

        public ScoreGameData ScoreGameData { get; set; }
        
        public ScoreGameController(WinLoseGame winLoseGame,
                                    ScoreGameUIProvider scoreGameUIProvider,
                                    IAnalyticService analyticService,
                                    ISaveService saveService)
        {
            _winLoseGame = winLoseGame;
            _scoreGameUIProvider = scoreGameUIProvider;
            _analyticService = analyticService;
            _saveService = saveService;
        }

        public async UniTask InitializeLoadDataAsync()
        {
            _scoreGameUI = await _scoreGameUIProvider.Load();
            ScoreGameData = await _saveService.LoadAsync();
            _winLoseGame.OnWin += AddWinScore;
            _winLoseGame.OnLose += AddLoseScore;
            _winLoseGame.OnRemoveLose += RemoveLoseScore;
        }

        public void Tick()
        {
            if (_scoreGameUI!=null)
            {
                _scoreGameUI.Show();
            }
        }
        
        public void Dispose()
        {
            _winLoseGame.OnWin -= AddWinScore;
            _winLoseGame.OnLose -= AddLoseScore;
            _winLoseGame.OnRemoveLose -= RemoveLoseScore;
        }
        
        public void InitializeScoreGameData()
        {
            ScoreGameData = new ScoreGameData();
        }
        
        public void GetDataToShowScore(ScoreGameData scoreGameData)
        {
            _scoreGameUI.GiveScoreGameData(scoreGameData);
        }

        private async void AddWinScore()
        {
            ScoreGameData.AddWinScore();
            if (ScoreGameData.SceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                ScoreGameData.AddSceneIndex();
            }
            else
            {
                ScoreGameData.ChangeScoreGameData(ScoreGameData);
            }
            await SaveGameAsync();
            _analyticService.SendWinLevelEvent(ScoreGameData.WinScore);
        }

        private async void AddLoseScore()
        {
            ScoreGameData.AddLoseScore();
            await SaveGameAsync();
            _analyticService.SendLoseLevel(ScoreGameData.LoseScore);
        }

        private async void RemoveLoseScore()
        {
            ScoreGameData.RemoveLoseScore();
            await SaveGameAsync();
            _analyticService.SendLoseLevel(ScoreGameData.LoseScore);
        }
        
        private async UniTask SaveGameAsync()
        {
            await _saveService.SaveAsync(ScoreGameData);
        }
    }
}
