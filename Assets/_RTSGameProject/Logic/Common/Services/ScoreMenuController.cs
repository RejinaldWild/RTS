using System;
using _RTSGameProject.Logic.Common.SaveLoad;
using _RTSGameProject.Logic.Common.Score.Model;
using _RTSGameProject.Logic.Common.Score.View;

namespace _RTSGameProject.Logic.Common.Services
{
    public class ScoreMenuController
    {
        private ScoreMenuUI _scoreMenuUI;
        private WinLoseGame _winLoseGame;
        private SceneChanger _sceneChanger;
        private SaveSystem _saveSystem;
        
        public ScoreMenuController(ScoreMenuUI scoreMenuUI, SceneChanger sceneChanger, SaveSystem saveSystem)
        {
            _scoreMenuUI = scoreMenuUI;
            _scoreMenuUI.CreateId(Guid.NewGuid().ToString());
            _sceneChanger = sceneChanger;
            _saveSystem = saveSystem;
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
