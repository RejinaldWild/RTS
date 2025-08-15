using System;
using _RTSGameProject.Logic.Common.SaveLoad;
using _RTSGameProject.Logic.Common.Score.Model;
using Unity.Services.Authentication;
using UnityEngine;
using UnityEngine.Advertisements;
using Zenject;

namespace _RTSGameProject.Logic.Ads.UnityAds
{
    public class UnityAdsService : IAdsService, IUnityAdsInitializationListener, IInitializable
    {
        private const string ANDROID_ADS_ID = "5880775";
        private const string IOS_ADS_ID = "5880774";
        
        private readonly ISaveService _saveService;
        
        private string _gameId;
        private bool _isTesting = true;
        private UnityAdsInterstitial _interstitial;
        private UnityAdsRewarded _rewarded;
        private ScoreGameData _scoreGameData;
        
        public bool RewardedFullyWatched { get; set; }
        public bool IsPaidForRemovingAds { get; set; }

        public UnityAdsService(UnityAdsInterstitial unityAdsInterstitial, UnityAdsRewarded unityAdsRewarded, ISaveService saveService)
        {
            _interstitial = unityAdsInterstitial;
            _rewarded = unityAdsRewarded;
            _saveService = saveService;
        }

        public async void Initialize()
        {
            await _saveService.Initialize();
            if (await _saveService.IsSaveExist())
            {
                _scoreGameData = await _saveService.LoadAsync();
            }
            
            IsPaidForRemovingAds = false; // for tests!
            if (_scoreGameData != null)
            {
                _scoreGameData.IsRemovedAds = IsPaidForRemovingAds; // for tests!
            }
            //IsPaidForRemovingAds = _scoreGameData.IsRemovedAds;
            _gameId = ANDROID_ADS_ID;

            if (!Advertisement.isInitialized && Advertisement.isSupported)
            {
                Advertisement.Initialize(_gameId, _isTesting, this);
            }

            _interstitial.Initialize();
            _rewarded.Initialize();
            _interstitial.LoadAdvertisement();
            _rewarded.LoadAdvertisement();
            _rewarded.OnFullyWatch += OnFullyWatched;
            RewardedFullyWatched = false;
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

        public void ShowRewarded(Action onRewardedCallback)
        {
            RewardedFullyWatched = false;
            _rewarded.ShowAdvertisement(onRewardedCallback);
        }

        public async void RemoveAds()
        {
            IsPaidForRemovingAds = true;
            _scoreGameData.IsRemovedAds = IsPaidForRemovingAds;
            await _saveService.SaveAsync(_scoreGameData);
        }

        public void OnInitializationComplete()
        {
            Debug.Log("Ads initialized");
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.Log("Ads initialization have failed");
        }

        private void OnFullyWatched()
        {
            RewardedFullyWatched = true;
        }
    }
}