using System;
using System.Threading.Tasks;
using _RTSGameProject.Logic.Common.Score.Model;
using Firebase;
using Firebase.Extensions;
using UnityEngine;

namespace _RTSGameProject.Logic.SDK.Firebase
{
    public class FirebaseInitializer: ISDK
    {
        private readonly FirebaseEventer _firebaseEventer;

        public FirebaseInitializer(FirebaseEventer firebaseEventer)
        {
            _firebaseEventer = firebaseEventer;
        }
        
        public void Initialize()
        {
            FirebaseApp
                .CheckAndFixDependenciesAsync()
                .ContinueWithOnMainThread(OnDependencyStatusReceived);
        }

        public void Dispose()
        {
            _firebaseEventer.StopApplication();
        }
        
        public void BuiltUnit(int data)
        {
            _firebaseEventer.BuiltUnit(data);
        }

        public void BuiltExpensiveUnit(int data)
        {
            _firebaseEventer.BuiltExpensiveUnit(data);
        }

        public void WonLevel(int data)
        {
            _firebaseEventer.WonLevel(data);
        }

        public void LostLevel(int data)
        {
            _firebaseEventer.LostLevel(data);
        }

        public void EnemyKilled(int data)
        {
            _firebaseEventer.EnemyKilled(data);
        }

        public void UnitKilled(int data)
        {
            _firebaseEventer.UnitKilled(data);
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
                
                Debug.Log($"All dependencies resolved successfully!");
                _firebaseEventer.StartApplication();
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }
    }
}