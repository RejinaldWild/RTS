using _RTSGameProject.Logic.Common.Score.Model;
using _RTSGameProject.Logic.Common.Score.View;

namespace _RTSGameProject.Logic.Common.Services
{
    public class ScoreController
    {
        private ScoreUI _scoreView;
        private ScoreData _scoreData;
        private WinLoseGame _winLoseGame;
        private ChangeScene _changeScene;
        private SaveSystem _saveSystem;
        private string _key;

        public ScoreController(ScoreUI scoreView, ScoreData scoreData, 
                                ChangeScene changeScene, SaveSystem saveSystem)
        {
            _scoreView = scoreView;
            _scoreData = scoreData;
            _changeScene = changeScene;
            _saveSystem = saveSystem;
            _changeScene.OnSceneLoad += OnSceneLoaded;
        }


        public ScoreController(ScoreUI scoreView, ScoreData scoreData, WinLoseGame winLoseGame, 
                                ChangeScene changeScene, SaveSystem saveSystem)
        {
            _scoreView = scoreView;
            _scoreData = scoreData;
            _winLoseGame = winLoseGame;
            _changeScene = changeScene;
            _saveSystem = saveSystem;
            _winLoseGame.OnWin += AddWinScore;
            _winLoseGame.OnLose += AddLoseScore;
        }

        public void Unsubscribe()
        {
            _winLoseGame.OnWin -= AddWinScore;
            _winLoseGame.OnLose -= AddLoseScore;
        }

        public void Show()
        {
            _scoreView.scoreText.text = $"Score - Win: {_scoreData.WinScore} - Lose: {_scoreData.LoseScore}";
        }

        private void AddWinScore()
        {
            _key = _scoreView.Id;
            _scoreData.WinScore++;
            _saveSystem.SaveGame(_key, _scoreData);
        }

        private void AddLoseScore()
        {
            _key = _scoreView.Id;
            _scoreData.LoseScore++;
            _saveSystem.SaveGame(_key, _scoreData);
        }
        
        private void OnSceneLoaded()
        {
            _key = _scoreView.Id;
            _saveSystem.LoadGame(_key);
        }
    }
}
