using System;
using _RTSGameProject.Logic.Common.SaveLoad;
using _RTSGameProject.Logic.Common.Score.Model;
using _RTSGameProject.Logic.Common.Score.View;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _RTSGameProject.Logic.Common.Services
{
    public class ScoreMenuController: ITickable, IDisposable
    {
        private readonly ScoreMenuUI _scoreMenuUI;
        private readonly SaveScoreService _saveScoreService;
        public ScoreGameData ScoreGameData { get; set; }

        public ScoreMenuController(ScoreMenuUI scoreMenuUI,
                                    SaveScoreService saveScoreService)
        {
            _scoreMenuUI = scoreMenuUI;
            _saveScoreService = saveScoreService;
        }

        public void InitializeScoreGameData()
        {
            ScoreGameData = new ScoreGameData();
        }
        
        public async UniTask LoadDataAsync()
        {
            ScoreGameData = await _saveScoreService.LoadAsync();
            
            // _scoreGameData.OnScoreGameDataChange += OnScoreGameDataChanged;
            // _sceneChanger.OnSceneLoad += OnSceneChangerLoaded;
        }

        public void Tick()
        {
            _scoreMenuUI.Show();
        }
        
        public void GetDataToShowScore(ScoreGameData scoreGameData)
        {
            _scoreMenuUI.GiveScoreGameData(scoreGameData);
        }
        
        public async UniTask DeleteSaves()
        {
            await _saveScoreService.DeleteAsync();
            _scoreMenuUI.GiveScoreGameData(new ScoreGameData());
        }
        
        public void Dispose()
        {
            // _scoreGameData.OnScoreGameDataChange -= OnScoreGameDataChanged;
            // _sceneChanger.OnSceneLoad -= OnSceneChangerLoaded;
        }
    }
}
