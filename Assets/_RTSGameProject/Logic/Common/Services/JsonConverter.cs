using _RTSGameProject.Logic.Common.Score.Model;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;

namespace _RTSGameProject.Logic.Common.Services
{
    public class JsonConverter: ISerializer
    {
        public UniTask<string> ToJsonAsync(ISaveData data)
        {
            string json = JsonConvert.SerializeObject(data);
            return UniTask.FromResult(json);
        }

        public UniTask<ScoreGameData> FromJsonAsync(string serializedData)
        {
            var data = JsonConvert.DeserializeObject<ScoreGameData>(serializedData);
            return UniTask.FromResult(data);
        }
    }
}
