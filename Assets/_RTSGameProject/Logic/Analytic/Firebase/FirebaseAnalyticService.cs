using System;
using System.Threading.Tasks;
using _RTSGameProject.Logic.Common.Services;
using Firebase;
using Firebase.Analytics;
using Firebase.Extensions;
using UnityEngine;
using Zenject;

namespace _RTSGameProject.Logic.Analytic.Firebase
{
    public class FirebaseAnalyticService: IAnalyticService, IInitializable, IDisposable
    {
        private readonly IRemoteConfigProvider _remoteConfigProvider;
        
        public FirebaseAnalyticService(IRemoteConfigProvider remoteConfigProvider)
        {
            _remoteConfigProvider = remoteConfigProvider;
        }
        
        public void Initialize()
        {
            FirebaseApp
                .CheckAndFixDependenciesAsync()
                .ContinueWithOnMainThread(OnDependencyStatusReceived);
            
        }
        
        public void Dispose()
        {
            FirebaseAnalytics.LogEvent("StopApp" , new Parameter("StopApplication", 0));
        }
        
        public void SendBuildUnit(int data)
        {
            FirebaseAnalytics.LogEvent("BuiltUnits" , new Parameter("BuiltUnitsFromHouse", data));
        }

        public void SendBuildExpensiveUnit(int data)
        {
            FirebaseAnalytics.LogEvent("BuiltExpUnits" , new Parameter("BuiltExpensiveUnits", data));
        }

        public void SendWinLevelEvent(int data)
        {
            FirebaseAnalytics.LogEvent("Wins" , new Parameter("WinScore", data));
        }

        public void SendLoseLevel(int data)
        {
            FirebaseAnalytics.LogEvent("Loses" , new Parameter("LoseScore", data));
        }

        public void SendKillEnemy(int data)
        {
            FirebaseAnalytics.LogEvent("Kills" , new Parameter("KilledEnemies", data));
        }

        public void SendKillUnit(int data)
        {
            FirebaseAnalytics.LogEvent("Casualties", new Parameter("UnitCasualties", data));
        }

        private void OnDependencyStatusReceived(Task<DependencyStatus> task)
        {
            try
            {
                if (!task.IsCompletedSuccessfully)
                {
                    throw new Exception(task.Exception.Message);
                }
                
                var status = task.Result;
                if (status != DependencyStatus.Available)
                {
                    throw new Exception($"Couldn't resolve all Firebase dependencies:{status}");
                }

                _remoteConfigProvider.FetchDataAsync();
                Debug.Log($"All dependencies resolved successfully!");
                FirebaseAnalytics.LogEvent("StartApp" , new Parameter("StartApplication", 1));
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }
    }
}