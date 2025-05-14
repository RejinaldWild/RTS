using System;
using _RTSGameProject.Logic.Common.SaveLoad;
using _RTSGameProject.Logic.Common.Score.Model;
using _RTSGameProject.Logic.Common.Score.View;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _RTSGameProject.Logic.Common.Services
{
    public class ScoreGameController: IInitializable, IDisposable, ITickable
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

        public void Initialize()
        {
            _winLoseGame.OnWin += AddWinScore;
            _winLoseGame.OnLose += AddLoseScore;
        }

        public void Tick()
        {
            _scoreGameUI.Show();
        }
        
        public void Dispose()
        {
            _winLoseGame.OnWin -= AddWinScore;
            _winLoseGame.OnLose -= AddLoseScore;
        }
        
        public async UniTask LoadData()
        {
            _scoreGameData = await _saveSystem.LoadAsync<ScoreGameData>();
            _scoreGameUI.GiveScoreGameData(_scoreGameData);
        }

        private void AddWinScore()
        {
            _scoreGameData.WinScore++;
            _scoreGameData.SceneIndex++;
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
