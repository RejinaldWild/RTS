using _RTSGameProject.Logic.Common.Score.Model;
using Cysharp.Threading.Tasks;

namespace _RTSGameProject.Logic.Common.SaveLoad
{
    public interface ISaveService
    {
        public bool IsSaveExist();
        public UniTask SaveAsync(ScoreGameData data);
        public UniTask<ScoreGameData> LoadAsync();
        public UniTask DeleteAsync();
    }
}