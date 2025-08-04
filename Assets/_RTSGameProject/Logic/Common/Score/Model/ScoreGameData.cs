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
        public DateTime DateTime { get; set; }

        public void AddWinScore()
        {
            WinScore++;
            DateTime = DateTime.Now;
        }

        public void AddLoseScore()
        {
            LoseScore++;
            DateTime = DateTime.Now;
        }

        public void RemoveLoseScore()
        {
            LoseScore--;
            DateTime = DateTime.Now;
        }
        
        public void AddSceneIndex()
        {
            SceneIndex++;
            DateTime = DateTime.Now;
        }

        public void RemoveAds()
        {
            IsRemovedAds = true;
            DateTime = DateTime.Now;
        }
        
        public void ChangeScoreGameData(ScoreGameData scoreGameData)
        {
            WinScore = scoreGameData.WinScore;
            LoseScore = scoreGameData.LoseScore;
            SceneIndex = scoreGameData.SceneIndex;
            DateTime = DateTime.Now;
        }
    }
}