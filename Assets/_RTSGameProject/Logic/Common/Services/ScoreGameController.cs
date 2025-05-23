using System;
using _RTSGameProject.Logic.Common.SaveLoad;
using _RTSGameProject.Logic.Common.Score.Model;
using _RTSGameProject.Logic.Common.Score.View;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using Zenject;

namespace _RTSGameProject.Logic.Common.Services
{
    public class ScoreGameController: IInitializable, IDisposable, ITickable
    {
        private readonly WinLoseGame _winLoseGame;
        private readonly SaveScoreService _saveScoreService;
        
        private ScoreGameData _scoreGameData;
        private ScoreGameUI _scoreGameUI;
        private string _key;
        
        public ScoreGameController(ScoreGameUI scoreGameUI, ScoreGameData scoreGameData, 
                                WinLoseGame winLoseGame, SaveScoreService saveScoreService)
        {
            _scoreGameUI = scoreGameUI;
            _scoreGameUI.CreateId(Guid.NewGuid().ToString());
            _scoreGameData = scoreGameData;
            _winLoseGame = winLoseGame;
            _saveScoreService = saveScoreService;
        }

        public void Initialize()
        {
            _winLoseGame.OnWin += AddWinScore;
            _winLoseGame.OnLose += AddLoseScore;
        }

        public void Tick()
        {
            _scoreGameUI.Show();
        }
        
        public void Dispose()
        {
            _winLoseGame.OnWin -= AddWinScore;
            _winLoseGame.OnLose -= AddLoseScore;
        }
        
        public async UniTask LoadData(ScoreGameUI scoreGameUI)
        {
            _scoreGameUI = scoreGameUI;
            _scoreGameData = await _saveScoreService.LoadAsync();
            _scoreGameUI.GiveScoreGameData(_scoreGameData);
        }

        private async void AddWinScore()
        {
            _scoreGameData.WinScore++;
            if (_scoreGameData.SceneIndex < SceneManager.sceneCountInBuildSettings-1)
            {
                _scoreGameData.SceneIndex++;
            }
            await SaveGameAsync();
        }

        private async void AddLoseScore()
        {
            _scoreGameData.LoseScore++;
            await SaveGameAsync();
        }

        private async UniTask SaveGameAsync()
        {
            await _saveScoreService.SaveAsync(_scoreGameData);
        }
    }
}
