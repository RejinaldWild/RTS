using System;
using _RTSGameProject.Logic.Common.SaveLoad;
using _RTSGameProject.Logic.Common.Score.Model;
using _RTSGameProject.Logic.Common.Score.View;
using Cysharp.Threading.Tasks;

namespace _RTSGameProject.Logic.Common.Services
{
    public class ScoreMenuController
    {
        private ScoreMenuUI _scoreMenuUI;
        private WinLoseGame _winLoseGame;
        private SceneChanger _sceneChanger;
        private SaveSystem _saveSystem;
        private ScoreGameData _scoreGameData;

        public ScoreGameData ScoreGameData => _scoreGameData;
        
        public ScoreMenuController(ScoreMenuUI scoreMenuUI, ScoreGameData scoreGameData, SceneChanger sceneChanger, SaveSystem saveSystem)
        {
            _scoreMenuUI = scoreMenuUI;
            _scoreGameData = scoreGameData;
            _scoreMenuUI.CreateId(Guid.NewGuid().ToString());
            _sceneChanger = sceneChanger;
            _saveSystem = saveSystem;
        }

        public async UniTask LoadData()
        {
            _scoreGameData = await _saveSystem.LoadAsync<ScoreGameData>();
            _scoreGameData.SceneIndex = _sceneChanger.MainMenuSceneIndex;
            _scoreMenuUI.GiveScoreGameData(_scoreGameData);
        }
        
        public void Subscribe()
        {
            _sceneChanger.OnSceneLoad += OnSceneChangerLoaded;
        }
        
        public void Unsubscribe()
        {
            _sceneChanger.OnSceneLoad -= OnSceneChangerLoaded;
        }

        public void Update()
        {
            _scoreMenuUI.Show();
        }
        
        private void OnSceneChangerLoaded()
        {
            _saveSystem.LoadAsync<ScoreGameData>();
        }
    }
}
