using System;
using _RTSGameProject.Logic.Common.SaveLoad;
using _RTSGameProject.Logic.Common.Score.Model;
using _RTSGameProject.Logic.Common.Score.View;
using Cysharp.Threading.Tasks;

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

        public void Update()
        {
            _scoreGameUI.Show();
        }

        private void AddWinScore()
        {
            _scoreGameData.WinScore++;
            SaveGameAsync();
        }

        private void AddLoseScore()
        {
            _scoreGameData.LoseScore++;
            SaveGameAsync();
        }

        private async UniTask SaveGameAsync()
        {
            await _saveSystem.SaveAsync(_scoreGameData);
        }
    }
}
