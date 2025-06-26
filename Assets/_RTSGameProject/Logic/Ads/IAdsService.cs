namespace _RTSGameProject.Logic.Ads
{
    public interface IAdsService
    {
        public void LoadInterstitial();
        public void ShowInterstitial();
        public void LoadRewarded();
        public void ShowRewarded();
        public bool RewardedFullyWatched { get; set; }
    }
}