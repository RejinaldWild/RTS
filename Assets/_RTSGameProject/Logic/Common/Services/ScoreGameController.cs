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
        private SceneChanger _sceneChanger;
        private ScoreGameUI _scoreGameUI;
        private string _key;

        public ScoreGameController(ScoreGameData scoreGameData, 
                                    WinLoseGame winLoseGame,
                                    SceneChanger sceneChanger,
                                    SaveScoreService saveScoreService)
        {
            _scoreGameData = scoreGameData;
            _winLoseGame = winLoseGame;
            _sceneChanger = sceneChanger;
            _saveScoreService = saveScoreService;
        }

        public void Initialize()
        {
            // _scoreGameData = await _saveScoreService.LoadAsync();
            // _scoreGameData.OnScoreGameDataChange += OnScoreGameDataChanged;
            
            _winLoseGame.OnWin += AddWinScore;
            _winLoseGame.OnLose += AddLoseScore;
        }

        private void OnScoreGameDataChanged(ScoreGameData scoreGameData)
        {
            //_scoreGameData = scoreGameData;
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
        
        public void LoadStartData()
        {
            _scoreGameUI.GiveScoreGameData(_scoreGameData);
        }
        
        public void LoadData(ScoreGameData scoreGameData)
        {
            _scoreGameData = scoreGameData;
            _scoreGameUI.GiveScoreGameData(scoreGameData);
            //_scoreGameData = await _saveScoreService.LoadAsync();
        }

        private async void AddWinScore()
        {
            _scoreGameData.AddWinScore();
            if (_scoreGameData.SceneIndex < SceneManager.sceneCountInBuildSettings-1)
            {
                _scoreGameData.AddSceneIndex();
            }
            else
            {
                _scoreGameData.ChangeScoreGameData(0);
            }
            await SaveGameAsync();
        }

        private async void AddLoseScore()
        {
            _scoreGameData.AddLoseScore();
            await SaveGameAsync();
        }

        private async UniTask SaveGameAsync()
        {
            await _saveScoreService.SaveAsync(_scoreGameData);
        }
    }
}
