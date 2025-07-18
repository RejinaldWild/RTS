namespace _RTSGameProject.Logic.Common.Services
{
    public interface IWinLoseActions
    {
        public void ToNextLevel();
        public void ToWatchRewardAd();
        public void ToContinueToPlay();
        public void ToMainMenu();
    }
}