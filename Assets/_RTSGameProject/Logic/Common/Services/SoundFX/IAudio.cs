using _RTSGameProject.Logic.Common.Config;

namespace _RTSGameProject.Logic.Common.Services.SoundFX
{
    public interface IAudio
    {
        public void PlayRandomSoundFX(SoundType soundFX);
        public void StartMusicPlaylist();
        public void StopMusicFX();
    }
}