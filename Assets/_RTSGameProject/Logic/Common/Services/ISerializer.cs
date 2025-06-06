using _RTSGameProject.Logic.Common.Score.Model;
using Cysharp.Threading.Tasks;

namespace _RTSGameProject.Logic.Common.Services
{
    public interface ISerializer
    {
        public UniTask<string> ToJsonAsync(ScoreGameData data);
        public UniTask<ScoreGameData> FromJsonAsync(string serializedData);
    }
}