using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _RTSGameProject.Logic.Common.Services
{
    public class PlayerPrefsDataStorage : IDataStorage
    {
        public UniTask<string> ReadAsync(string key)
        {
            string serilizedData = PlayerPrefs.GetString(key);
            return UniTask.FromResult(serilizedData);
        }
        
        public UniTask WriteAsync(string key, string serializedData)
        {
            PlayerPrefs.SetString(key, serializedData);
            return UniTask.CompletedTask;
        }

        public UniTask DeleteAsync(string key)
        {
            PlayerPrefs.DeleteKey(key);
            return UniTask.CompletedTask;
        }

        public UniTask<bool> ExistAsync(string key)
        {
            bool exist = PlayerPrefs.HasKey(key);
            return UniTask.FromResult(exist);
        }

        public UniTask Save()
        {
            PlayerPrefs.Save();
            return UniTask.CompletedTask;
        }
    }
}
