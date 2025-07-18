using System;

namespace _RTSGameProject.Logic.Common.Score.Model
{
    [Serializable]
    public class ScoreGameData
    {
        
        public int WinScore { get; set; }
        public int LoseScore { get; set; }
        public int SceneIndex { get; set; }
        public bool IsRemovedAds { get; set; }

        public void AddWinScore()
        {
            WinScore++;
        }

        public void AddLoseScore()
        {
            LoseScore++;
        }

        public void RemoveLoseScore()
        {
            LoseScore--;
        }
        
        public void AddSceneIndex()
        {
            SceneIndex++;
        }

        public void RemoveAds()
        {
            IsRemovedAds = true;
        }
        
        public void ChangeScoreGameData(ScoreGameData scoreGameData)
        {
            WinScore = scoreGameData.WinScore;
            LoseScore = scoreGameData.LoseScore;
            SceneIndex = scoreGameData.SceneIndex;
        }
    }
}