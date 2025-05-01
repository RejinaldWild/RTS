using _RTSGameProject.Logic.Common.Score.Model;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace _RTSGameProject.Logic.Common.Services
{
    public class JsonConverter: ISerializer
    {
        public UniTask<string> ToJsonAsync<TData>(TData data)
        {
            string json = JsonConvert.SerializeObject(data);
            return UniTask.FromResult(json);
        }

        public UniTask<TData> FromJsonAsync<TData>(string serializedData)
        {
            var data = JsonConvert.DeserializeObject<TData>(serializedData);
            return UniTask.FromResult(data);
        }
    }
}
