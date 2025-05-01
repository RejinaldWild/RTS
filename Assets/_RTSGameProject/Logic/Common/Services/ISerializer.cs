using Cysharp.Threading.Tasks;

namespace _RTSGameProject.Logic.Common.Services
{
    public interface ISerializer
    {
        public UniTask<string> ToJsonAsync<TData>(TData data);
        public UniTask<TData> FromJsonAsync<TData>(string serializedData);
    }
}