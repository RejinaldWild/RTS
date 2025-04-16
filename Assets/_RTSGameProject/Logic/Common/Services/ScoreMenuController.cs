using System;
using _RTSGameProject.Logic.Common.Score.Model;
using _RTSGameProject.Logic.Common.Score.View;

namespace _RTSGameProject.Logic.Common.Services
{
    public class ScoreMenuController
    {
        private ScoreMenuUI _scoreMenuUI;
        private ScoreMenuData _scoreMenuData;
        private WinLoseGame _winLoseGame;
        private ChangeScene _changeScene;
        private SaveSystem _saveSystem;
        private string _key;
        
        public ScoreMenuController(ScoreMenuUI scoreMenuUI, ScoreMenuData scoreMenuData, 
                                ChangeScene changeScene, SaveSystem saveSystem)
        {
            _scoreMenuUI = scoreMenuUI;
            _scoreMenuUI.CreateId(Guid.NewGuid().ToString());
            _scoreMenuData = scoreMenuData;
            _changeScene = changeScene;
            _saveSystem = saveSystem;
            _changeScene.OnSceneLoad += OnSceneLoaded;
        }

        public void Unsubscribe()
        {
            _changeScene.OnSceneLoad -= OnSceneLoaded;
        }

        public void Show()
        {
            _scoreMenuUI.scoreText.text = $"Score - Win: {_scoreMenuData.WinScore} - Lose: {_scoreMenuData.LoseScore}";
        }
        
        private void OnSceneLoaded()
        {
            _key = _scoreMenuUI.Id;
            _saveSystem.LoadGame(_key);
        }
    }
}
