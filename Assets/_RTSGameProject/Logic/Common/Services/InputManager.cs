using System;
using UnityEngine;

namespace _RTSGameProject.Logic.Common.Services
{
    public class InputManager
    {
        public event Action<Ray> OnLeftClickMouseButtonDown;
        public event Action OnLeftClickMouseButton;
        public event Action OnLeftClickMouseButtonUp;
        public event Action<Ray> OnRightClickMouseButtonDown;
    
        private UnityEngine.Camera _camera;
    
        public InputManager(UnityEngine.Camera camera)
        {
            _camera = camera;
        }

        public void Update()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                OnLeftClickMouseButtonDown?.Invoke(ray);
            }

            if (UnityEngine.Input.GetMouseButton(0))
            {
                OnLeftClickMouseButton?.Invoke();
            }

            if (UnityEngine.Input.GetMouseButtonUp(0))
            {
                OnLeftClickMouseButtonUp?.Invoke();
            }
        
            if (UnityEngine.Input.GetMouseButtonDown(1))
            {
                OnRightClickMouseButtonDown?.Invoke(ray);
            }
        }
    }
}
