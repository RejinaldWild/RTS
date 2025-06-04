using System;

namespace _RTSGameProject.Logic.Common.Services
{
    public class PauseGame
    {
        public event Action OnPause;
        public event Action OnUnPause;
        
        public bool OnPaused { get; private set; }

        public void Pause()
        {
            OnPause?.Invoke();
            OnPaused = true;
        }

        public void UnPause()
        {
            OnUnPause?.Invoke();
            OnPaused = false;
        }
    }
}
