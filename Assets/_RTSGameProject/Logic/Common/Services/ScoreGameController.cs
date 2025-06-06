using System;
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
        private readonly SaveScoreService _saveScoreService;
        
        private ScoreGameUI _scoreGameUI;
        private string _key;

        public ScoreGameData ScoreGameData { get; set; }
        
        public ScoreGameController(WinLoseGame winLoseGame,
                                    ScoreGameUIProvider scoreGameUIProvider,
                                    SaveScoreService saveScoreService)
        {
            _winLoseGame = winLoseGame;
            _scoreGameUIProvider = scoreGameUIProvider;
            _saveScoreService = saveScoreService;
        }

        public async UniTask InitializeLoadDataAsync()
        {
            // _scoreGameData.OnScoreGameDataChange += OnScoreGameDataChanged;
            _scoreGameUI = await _scoreGameUIProvider.Load();
            ScoreGameData = await _saveScoreService.LoadAsync();
            _winLoseGame.OnWin += AddWinScore;
            _winLoseGame.OnLose += AddLoseScore;
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
            //_scoreGameData.OnScoreGameDataChange -= OnScoreGameDataChanged;
            _winLoseGame.OnWin -= AddWinScore;
            _winLoseGame.OnLose -= AddLoseScore;
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
        }

        private async void AddLoseScore()
        {
            ScoreGameData.AddLoseScore();
            await SaveGameAsync();
        }

        private async UniTask SaveGameAsync()
        {
            await _saveScoreService.SaveAsync(ScoreGameData);
        }
    }
}
