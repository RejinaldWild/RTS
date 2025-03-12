using System;
using _RTSGameProject.Logic.Common.Score.Model;
using _RTSGameProject.Logic.Common.Score.View;

namespace _RTSGameProject.Logic.Common.Services
{
    public class ScoreGameController
    {
        private ScoreGameUI _scoreGameView;
        private ScoreData _scoreData;
        private WinLoseGame _winLoseGame;
        private SaveSystem _saveSystem;
        private string _key;

        public ScoreGameController(ScoreGameUI scoreGameView, ScoreData scoreData, 
                                WinLoseGame winLoseGame, SaveSystem saveSystem)
        {
            _scoreGameView = scoreGameView;
            _scoreGameView.CreateId(Guid.NewGuid().ToString());
            _scoreData = scoreData;
            _winLoseGame = winLoseGame;
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
            _scoreGameView.scoreText.text = $"Score - Win: {_scoreData.WinScore} - Lose: {_scoreData.LoseScore}";
        }

        private void AddWinScore()
        {
            _key = _scoreGameView.Id;
            _scoreData.WinScore++;
            _saveSystem.SaveGame(_key, _scoreData);
        }

        private void AddLoseScore()
        {
            _key = _scoreGameView.Id;
            _scoreData.LoseScore++;
            _saveSystem.SaveGame(_key, _scoreData);
        }
    }
}
