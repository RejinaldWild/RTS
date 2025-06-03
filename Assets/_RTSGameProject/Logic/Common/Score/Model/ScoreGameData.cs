using System;

namespace _RTSGameProject.Logic.Common.Score.Model
{
    [Serializable]
    public class ScoreGameData: ISaveData
    {
        public event Action OnScoreGameDataChange;
        
        public int WinScore { get; private set; }
        public int LoseScore { get; private set; }
        public int SceneIndex { get; private set; }

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

        public void AddSceneIndex()
        {
            SceneIndex++;
            OnScoreGameDataChange?.Invoke();
        }

        public void ChangeScoreGameData(int sceneIndex)
        {
            SceneIndex = sceneIndex;
            OnScoreGameDataChange?.Invoke();
        }
    }
}