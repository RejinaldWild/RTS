using _RTSGameProject.Logic.Common.SaveLoad;
using _RTSGameProject.Logic.Common.Score.Model;
using _RTSGameProject.Logic.Common.Score.View;
using Zenject;

namespace _RTSGameProject.Logic.Common.Services
{
    public class ScoreMenuController: ITickable
    {
        private readonly ScoreMenuUI _scoreMenuUI;
        private readonly SaveScoreService _saveScoreService;
        private readonly ScoreGameData _scoreGameData;

        public ScoreMenuController(ScoreMenuUI scoreMenuUI,
                                    ScoreGameData scoreGameData,
                                    SaveScoreService saveScoreService)
        {
            _scoreMenuUI = scoreMenuUI;
            _saveScoreService = saveScoreService;
            _scoreGameData = scoreGameData;
        }
        
        // public async void Initialize()
        // {
        //     _scoreGameData = await _saveScoreService.LoadAsync();
        //     _scoreGameData.OnScoreGameDataChange += OnScoreGameDataChanged;
        //     _sceneChanger.OnSceneLoad += OnSceneChangerLoaded;
        // }

        // private void OnScoreGameDataChanged(ScoreGameData scoreGameData)
        // {
        //     _scoreGameData = scoreGameData;
        // }

        public void Tick()
        {
            _scoreMenuUI.Show();
        }
        
        public void GetDataToShowStartScore()
        {
            _scoreMenuUI.GiveScoreGameData(_scoreGameData);
        }
        
        public void GetDataToShowLoadedScore(ScoreGameData scoreGameData)
        {
            _scoreMenuUI.GiveScoreGameData(scoreGameData);
        }
        
        // public void Dispose()
        // {
        //     _scoreGameData.OnScoreGameDataChange -= OnScoreGameDataChanged;
        //     _sceneChanger.OnSceneLoad -= OnSceneChangerLoaded;
        // }
        
        // private async void OnSceneChangerLoaded()
        // {
        //     await _saveScoreService.LoadAsync();
        // }
    }
}
