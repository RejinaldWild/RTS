using System;
using System.Threading.Tasks;
using _RTSGameProject.Logic.Common.Config;
using Firebase.Extensions;
using Firebase.RemoteConfig;
using Newtonsoft.Json;
using UnityEngine;

namespace _RTSGameProject.Logic.Common.Services
{
    public class FirebaseRemoteConfigProvider: IRemoteConfigProvider
    {
        public WinLoseConfig WinLoseConfig { get; set; }
        public UnitConfig UnitConfig { get; set; }
        
        public Task FetchDataAsync()
        {
            Task fetchTask = FirebaseRemoteConfig.DefaultInstance.FetchAsync(TimeSpan.Zero);
            return fetchTask.ContinueWithOnMainThread(FetchComplete);
        }

        private void GetValues()
        {
            string winLoseConfig = FirebaseRemoteConfig.DefaultInstance.GetValue("WinLoseConfig").StringValue;
            WinLoseConfig = JsonConvert.DeserializeObject<WinLoseConfig>(winLoseConfig);
            
            string unitConfig = FirebaseRemoteConfig.DefaultInstance.GetValue("UnitConfig").StringValue;
            UnitConfig = JsonConvert.DeserializeObject<UnitConfig>(unitConfig);
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