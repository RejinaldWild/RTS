using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace _RTSGameProject.Logic.Ads.UnityAds
{
    public class UnityAdsRewarded: IUnityAdsLoadListener, IUnityAdsShowListener
    {
        public event Action OnRewardedAdFullyWatch;
        
        private const string ANDROID_AD_ID = "Rewarded_Android";
        private const string IOS_AD_ID = "Rewarded_iOS";
        private string _rewardedAdId;
        
        public void Initialize()
        {
            _rewardedAdId = ANDROID_AD_ID;
        }

        public void LoadAdvertisement()
        {
            Advertisement.Load(_rewardedAdId, this);
        }

        public void ShowAdvertisement()
        {
            Advertisement.Show(_rewardedAdId, this);
            LoadAdvertisement();
        }

        public void OnUnityAdsAdLoaded(string placementId)
        {
            Debug.Log("Rewarded Ad Loaded");
        }

        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            Debug.Log("Rewarded Ad has failed to load");
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message) {}

        public void OnUnityAdsShowStart(string placementId){}

        public void OnUnityAdsShowClick(string placementId){}

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            if (placementId == _rewardedAdId && showCompletionState == UnityAdsShowCompletionState.COMPLETED)
            {
                OnRewardedAdFullyWatch?.Invoke();
                Debug.Log("Ads are fully watched");
            }
        }
    }
}