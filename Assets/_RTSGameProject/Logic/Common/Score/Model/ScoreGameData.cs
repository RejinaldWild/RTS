using System;

namespace _RTSGameProject.Logic.Common.Score.Model
{
    [Serializable]
    public class ScoreGameData
    {
        public event Action OnScoreGameDataChange;
        
        public int WinScore { get; set; }
        public int LoseScore { get; set; }
        public int SceneIndex { get; set; }

        public void AddWinScore()
        {
            WinScore++;
            OnScoreGameDataChange?.Invoke();
        }

        public void AddLoseScore()
        {
            LoseScore++;
            OnScoreGameDataChange?.Invoke();
        }

        public void RemoveLoseScore()
        {
            LoseScore--;
            OnScoreGameDataChange?.Invoke();
        }
        
        public void AddSceneIndex()
        {
            SceneIndex++;
            OnScoreGameDataChange?.Invoke();
        }

        public void ChangeScoreGameData(ScoreGameData scoreGameData)
        {
            WinScore = scoreGameData.WinScore;
            LoseScore = scoreGameData.LoseScore;
            SceneIndex = scoreGameData.SceneIndex;
            OnScoreGameDataChange?.Invoke();
        }
    }
}