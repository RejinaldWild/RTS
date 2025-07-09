using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace _RTSGameProject.Logic.Ads.UnityAds
{
    public class UnityAdsRewarded: IUnityAdsLoadListener, IUnityAdsShowListener
    {
        public event Action OnFullyWatch;
        
        private const string ANDROID_AD_ID = "Rewarded_Android";
        private const string IOS_AD_ID = "Rewarded_iOS";
        private string _rewardedAdId;
        private Action _onRewardedCallback;
        
        public void Initialize()
        {
            _rewardedAdId = ANDROID_AD_ID;
        }

        public void LoadAdvertisement()
        {
            Advertisement.Load(_rewardedAdId, this);
        }

        public void ShowAdvertisement(Action onRewardedCallback)
        {
            _onRewardedCallback = onRewardedCallback;
            Advertisement.Show(_rewardedAdId, this);
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
                OnFullyWatch?.Invoke();
                _onRewardedCallback?.Invoke();
                _onRewardedCallback = null;
                Debug.Log("Ads are fully watched");
            }
        }
    }
}