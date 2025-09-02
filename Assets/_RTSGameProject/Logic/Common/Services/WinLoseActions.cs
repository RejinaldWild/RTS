using _RTSGameProject.Logic.Common.Config;
using _RTSGameProject.Logic.Common.Services.SoundFX;

namespace _RTSGameProject.Logic.Common.Services
{
    public class WinLoseActions : IWinLoseActions
    {
        private readonly WinLoseGame _winLoseGame;
        private readonly IAudio _audioService;

        public WinLoseActions(WinLoseGame winLoseGame, IAudio audioService)
        {
            _winLoseGame = winLoseGame;
            _audioService = audioService;
        }

        public void ToNextLevel()
        {
            _audioService.PlayRandomSoundFX(SoundType.CLICK);
            _winLoseGame.ToNextLevel();
        }

        public void ToWatchRewardAd()
        {
            _audioService.PlayRandomSoundFX(SoundType.CLICK);
            _winLoseGame.ToWatchRewardAd();
        }

        public void ToContinueToPlay()
        {
            _audioService.PlayRandomSoundFX(SoundType.CLICK);
            _winLoseGame.ToContinueToPlay();
        }

        public void ToMainMenu()
        {
            _audioService.PlayRandomSoundFX(SoundType.CLICK);
            _winLoseGame.ToMainMenu();
        }
    }
}