namespace _RTSGameProject.Logic.Common.Services
{
    public class WinLoseActions : IWinLoseActions
    {
        private readonly WinLoseGame _winLoseGame;

        public WinLoseActions(WinLoseGame winLoseGame)
        {
            _winLoseGame = winLoseGame;
        }

        public void ToNextLevel()
        {
            _winLoseGame.ToNextLevel();
        }

        public void ToWatchRewardAd()
        {
            _winLoseGame.ToWatchRewardAd();
        }

        public void ToContinueToPlay()
        {
            _winLoseGame.ToContinueToPlay();
        }

        public void ToMainMenu()
        {
            _winLoseGame.ToMainMenu();
        }
    }
}