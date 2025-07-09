using UnityEngine;
using UnityEngine.Advertisements;

namespace _RTSGameProject.Logic.Ads.UnityAds
{
    public class UnityAdsInterstitial: IUnityAdsLoadListener, IUnityAdsShowListener
    {
        private const string ANDROID_AD_ID = "Interstitial_Android";
        private const string IOS_AD_ID = "Interstitial_iOS";
        private string _interstitialAdId;
        
        public void Initialize()
        {
            _interstitialAdId = ANDROID_AD_ID;
        }
        
        public void LoadAdvertisement()
        {
            Advertisement.Load(_interstitialAdId, this);
        }

        public void ShowAdvertisement()
        {
            Advertisement.Show(_interstitialAdId, this);
        }

        public void OnUnityAdsAdLoaded(string placementId)
        {
            Debug.Log("Interstitial Ad Loaded");
        }

        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            Debug.Log("Interstitial Ad has failed to load");
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message) { }

        public void OnUnityAdsShowStart(string placementId) { }

        public void OnUnityAdsShowClick(string placementId) { }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            Debug.Log("Interstitial Ad show is completed");
        }
    }
}