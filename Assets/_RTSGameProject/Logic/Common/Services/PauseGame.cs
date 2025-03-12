using System;
using _RTSGameProject.Logic.Common.Character.Model;
using _RTSGameProject.Logic.Common.View;
using UnityEngine;

namespace _RTSGameProject.Logic.Common.Services
{
    public class PauseGame
    {
        public event Action OnPause;
        public event Action OnUnPause ; 
        
        private UnitsRepository _unitsRepository;
        private WinLoseWindow _winLoseWindow;
        
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
