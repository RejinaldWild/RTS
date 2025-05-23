using System;
using _RTSGameProject.Logic.Common.SaveLoad;
using _RTSGameProject.Logic.Common.Score.Model;
using _RTSGameProject.Logic.Common.Score.View;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _RTSGameProject.Logic.Common.Services
{
    public class ScoreMenuController: IInitializable, IDisposable, ITickable
    {
        private readonly ScoreMenuUI _scoreMenuUI;
        private readonly WinLoseGame _winLoseGame;
        private readonly SceneChanger _sceneChanger;
        private readonly SaveScoreService _saveScoreService;
        private ScoreGameData _scoreGameData;
        
        public ScoreMenuController(ScoreMenuUI scoreMenuUI, ScoreGameData scoreGameData, SceneChanger sceneChanger, SaveScoreService saveScoreService)
        {
            _scoreMenuUI = scoreMenuUI;
            _scoreGameData = scoreGameData;
            _scoreMenuUI.CreateId(Guid.NewGuid().ToString());
            _sceneChanger = sceneChanger;
            _saveScoreService = saveScoreService;
        }
        
        public void Initialize()
        {
            _sceneChanger.OnSceneLoad += OnSceneChangerLoaded;
        }

        public void Tick()
        {
            _scoreMenuUI.Show();
        }
        
        public async UniTask LoadData()
        {
            _scoreGameData = await _saveScoreService.LoadAsync();
            _scoreGameData.SceneIndex = _sceneChanger.MainMenuSceneIndex;
            _scoreMenuUI.GiveScoreGameData(_scoreGameData);
        }
        
        public void Dispose()
        {
            _sceneChanger.OnSceneLoad -= OnSceneChangerLoaded;
        }
        
        private async void OnSceneChangerLoaded()
        {
            await _saveScoreService.LoadAsync();
        }
    }
}
