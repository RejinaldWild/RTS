using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _RTSGameProject.Logic.Common.SaveLoad
{
    public class PlayerPrefsDataStorage : IDataStorage
    {
        public UniTask<string> ReadAsync(string key)
        {
            string serializedData = PlayerPrefs.GetString(key);
            return UniTask.FromResult(serializedData);
        }
        
        public UniTask WriteAsync(string key,string serializedData)
        {
            PlayerPrefs.SetString(key, serializedData);
            return UniTask.CompletedTask;
        }

        public UniTask DeleteAsync(string key)
        {
            PlayerPrefs.DeleteKey(key);
            return UniTask.CompletedTask;
        }

        public bool Exist(string key)
        {
            return PlayerPrefs.HasKey(key);
        }

        public UniTask Save()
        {
            PlayerPrefs.Save();
            return UniTask.CompletedTask;
        }
    }
}
