using _RTSGameProject.Logic.Common.Score.Model;
using Cysharp.Threading.Tasks;

namespace _RTSGameProject.Logic.Common.SaveLoad
{
    public interface ISaveScoreService
    {
        UniTask SaveAsync(ISaveData data);
        UniTask<ScoreGameData> LoadAsync();
    }
}