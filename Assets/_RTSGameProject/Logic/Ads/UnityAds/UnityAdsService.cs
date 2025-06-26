using System;
using UnityEngine;
using UnityEngine.Advertisements;
using Zenject;

namespace _RTSGameProject.Logic.Ads.UnityAds
{
    public class UnityAdsService: IAdsService, IUnityAdsInitializationListener, IInitializable, IDisposable
    {
        private const string ANDROID_ADS_ID = "5880775";
        private const string IOS_ADS_ID = "5880774";
        private string _gameId;
        private bool _isTesting = true;
        private UnityAdsInterstitial _interstitial;
        private UnityAdsRewarded _rewarded;
        
        public bool RewardedFullyWatched { get; set; }

        public UnityAdsService(UnityAdsInterstitial unityAdsInterstitial, UnityAdsRewarded unityAdsRewarded)
        {
            _interstitial = unityAdsInterstitial;
            _rewarded = unityAdsRewarded;
        }
        
        public void Initialize()
        {
            _gameId = ANDROID_ADS_ID;

            if (!Advertisement.isInitialized && Advertisement.isSupported)
            {
                Advertisement.Initialize(_gameId,_isTesting,this);
            }
            _interstitial.Initialize();
            _rewarded.Initialize();
            _interstitial.LoadAdvertisement();
            _rewarded.LoadAdvertisement();
            _rewarded.OnRewardedAdFullyWatch += OnRewardedAdFullyWatched;
            RewardedFullyWatched = false;
        }

        public void Dispose()
        {
            _rewarded.OnRewardedAdFullyWatch -= OnRewardedAdFullyWatched;
        }
        
        public void LoadInterstitial()
        {
            _interstitial.LoadAdvertisement();
        }

        public void ShowInterstitial()
        {
            _interstitial.ShowAdvertisement();
        }

        public void LoadRewarded()
        {
            _rewarded.LoadAdvertisement();
        }

        public void ShowRewarded()
        {
            RewardedFullyWatched = false;
            _rewarded.ShowAdvertisement();
            RewardedFullyWatched = true;
        }

        public void OnInitializationComplete()
        {
            Debug.Log("Ads initialized");
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.Log("Ads initialization have failed");
        }
        
        private void OnRewardedAdFullyWatched()
        {
            RewardedFullyWatched = true;
        }

    }
}