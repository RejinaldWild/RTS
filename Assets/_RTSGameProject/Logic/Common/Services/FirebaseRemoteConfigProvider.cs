using System;
using System.Threading.Tasks;
using _RTSGameProject.Logic.Common.Config;
using Firebase.Extensions;
using Firebase.RemoteConfig;
using Newtonsoft.Json;
using UnityEngine;

namespace _RTSGameProject.Logic.Common.Services
{
    public class FirebaseRemoteConfigProvider
    {
        public WinLoseConfig WinLoseConfig { get;private set; }
        public UnitConfig UnitConfig { get;private set; }
        
        public Task FetchDataAsync()
        {
            Task fetchTask = FirebaseRemoteConfig.DefaultInstance.FetchAsync(TimeSpan.Zero);
            return fetchTask.ContinueWithOnMainThread(FetchComplete);
        }

        public void GetValues()
        {
            WinLoseConfig = JsonConvert.DeserializeObject<WinLoseConfig>
                (FirebaseRemoteConfig.DefaultInstance.GetValue("WinLoseConfig").StringValue);
            Debug.Log(WinLoseConfig);
            
            UnitConfig = JsonConvert.DeserializeObject<UnitConfig>
                (FirebaseRemoteConfig.DefaultInstance.GetValue("UnitConfig").StringValue);
            Debug.Log(UnitConfig);
        }
        
        private void FetchComplete(Task fetchTask)
        {
            if (!fetchTask.IsCompleted)
            {
                Debug.LogError("Fetch task is not completed");
                return;
            }
            
            var remoteConfig = FirebaseRemoteConfig.DefaultInstance;
            var info = remoteConfig.Info;
            if (info.LastFetchStatus != LastFetchStatus.Success)
            {
                Debug.LogError($"{nameof(FetchComplete)} was unsuccessful: {nameof(info.LastFetchStatus)} : {info.LastFetchStatus}");
                return;
            }

            remoteConfig.ActivateAsync()
                .ContinueWithOnMainThread(task =>
                {
                    Debug.Log($"Remote config is loaded and ready to use. Last fetch time is {info.FetchTime}");
                    GetValues();
                });
        }
    }
}