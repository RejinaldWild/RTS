using System;

namespace _RTSGameProject.Logic.Ads
{
    public interface IAdsService
    {
        public void LoadInterstitial();
        public void ShowInterstitial();
        public void LoadRewarded();
        public void ShowRewarded(Action onRewardedCallback);
        public bool RewardedFullyWatched { get; set; }
    }
}