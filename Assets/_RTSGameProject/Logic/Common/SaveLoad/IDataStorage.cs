using Cysharp.Threading.Tasks;

namespace _RTSGameProject.Logic.Common.SaveLoad
{
    public interface IDataStorage
    {
        public UniTask<string> ReadAsync(string key);
        public UniTask WriteAsync(string key, string serializedData);
        public UniTask DeleteAsync(string key);
        public bool Exist(string key);
    }
}
