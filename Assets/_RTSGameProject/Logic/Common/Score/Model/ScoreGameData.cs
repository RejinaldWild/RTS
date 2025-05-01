using System;

namespace _RTSGameProject.Logic.Common.Score.Model
{
    [Serializable]
    public class ScoreGameData: ISaveData
    {
        public int WinScore { get; set; }
        public int LoseScore { get; set; }
    }
}