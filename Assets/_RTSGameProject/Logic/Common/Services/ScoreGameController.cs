using System;
using _RTSGameProject.Logic.Common.Score.Model;
using _RTSGameProject.Logic.Common.Score.View;
using Zenject;

namespace _RTSGameProject.Logic.Common.Services
{
    public class ScoreGameController
    {
        private ScoreGameUI _scoreGameUI;
        private ScoreGameData _scoreGameData;
        private WinLoseGame _winLoseGame;
        private SaveSystem _saveSystem;
        private string _key;
        
        public ScoreGameController(ScoreGameUI scoreGameUI, ScoreGameData scoreGameData, 
                                WinLoseGame winLoseGame, SaveSystem saveSystem)
        {
            _scoreGameUI = scoreGameUI;
            _scoreGameUI.CreateId(Guid.NewGuid().ToString());
            _scoreGameData = scoreGameData;
            _winLoseGame = winLoseGame;
            _saveSystem = saveSystem;
        }

        public void Subscribe()
        {
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
            _scoreGameUI.scoreText.text = $"Score - Win: {_scoreGameData.WinScore} - Lose: {_scoreGameData.LoseScore}";
        }

        private void AddWinScore()
        {
            _key = _scoreGameUI.Id;
            _scoreGameData.WinScore++;
            _saveSystem.SaveGame(_key, _scoreGameData);
        }

        private void AddLoseScore()
        {
            _key = _scoreGameUI.Id;
            _scoreGameData.LoseScore++;
            _saveSystem.SaveGame(_key, _scoreGameData);
        }
    }
}
