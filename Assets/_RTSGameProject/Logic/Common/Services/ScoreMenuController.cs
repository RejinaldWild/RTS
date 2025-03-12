using System;
using _RTSGameProject.Logic.Common.Score.Model;
using _RTSGameProject.Logic.Common.Score.View;

namespace _RTSGameProject.Logic.Common.Services
{
    public class ScoreMenuController
    {
        private ScoreMenuUI _scoreMenuView;
        private ScoreData _scoreData;
        private WinLoseGame _winLoseGame;
        private ChangeScene _changeScene;
        private SaveSystem _saveSystem;
        private string _key;

        public ScoreMenuController(ScoreMenuUI scoreMenuView, ScoreData scoreData, 
                                ChangeScene changeScene, SaveSystem saveSystem)
        {
            _scoreMenuView = scoreMenuView;
            _scoreMenuView.CreateId(Guid.NewGuid().ToString());
            _scoreData = scoreData;
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
            _scoreMenuView.scoreText.text = $"Score - Win: {_scoreData.WinScore} - Lose: {_scoreData.LoseScore}";
        }
        
        private void OnSceneLoaded()
        {
            _key = _scoreMenuView.Id;
            _saveSystem.LoadGame(_key);
        }
    }
}
