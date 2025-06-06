﻿using _RTSGameProject.Logic.Common.Score.Model;
using Cysharp.Threading.Tasks;

namespace _RTSGameProject.Logic.Common.SaveLoad
{
    public interface ISaveScoreService
    {
        UniTask SaveAsync(ScoreGameData data);
        UniTask<ScoreGameData> LoadAsync();
    }
}