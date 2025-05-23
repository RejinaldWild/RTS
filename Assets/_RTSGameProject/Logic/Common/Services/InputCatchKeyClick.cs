using System;
using UnityEngine;
using Zenject;

namespace _RTSGameProject.Logic.Common.Services
{
    public class InputCatchKeyClick: ITickable
    {
        public event Action<Ray> OnLeftClickMouseButton;
        public event Action<Ray> OnLeftClickMouseButtonHold;
        public event Action OnLeftClickMouseButtonUp;
        public event Action<Ray> OnRightClickMouseButtonDown;
        public event Action OnEscPress;
        public event Action OnEscPressAgain; 
    
        private Camera _camera;
        private PauseGame _pauseGame;
    
        public InputCatchKeyClick(Camera camera, PauseGame pauseGame)
        {
            _camera = camera;
            _pauseGame = pauseGame;
        }

        public void Tick()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        
            if (Input.GetMouseButtonDown(0))
            {
                OnLeftClickMouseButton?.Invoke(ray);
            }
            
            if (Input.GetMouseButton(0))
            {
                OnLeftClickMouseButtonHold?.Invoke(ray);
            }
            
            if (Input.GetMouseButtonUp(0))
            {
                OnLeftClickMouseButtonUp?.Invoke();
            }
        
            if (Input.GetMouseButtonDown(1))
            {
                OnRightClickMouseButtonDown?.Invoke(ray);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (_pauseGame.OnPaused)
                {
                    OnEscPressAgain?.Invoke();
                }
                else
                {
                    OnEscPress?.Invoke();
                }
            }
        }
    }
}
