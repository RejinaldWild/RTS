using System;
using UnityEngine;

namespace _RTSGameProject.Logic.Common.Services
{
    public class InputCatchKeyClick
    {
        public event Action<Ray> OnLeftClickMouseButtonDown;
        public event Action OnLeftClickMouseButton;
        public event Action OnLeftClickMouseButtonUp;
        public event Action<Ray> OnRightClickMouseButtonDown;
    
        private UnityEngine.Camera _camera;
    
        public InputCatchKeyClick(UnityEngine.Camera camera)
        {
            _camera = camera;
        }

        public void Update()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        
            if (Input.GetMouseButtonDown(0))
            {
                OnLeftClickMouseButtonDown?.Invoke(ray);
            }

            if (Input.GetMouseButton(0))
            {
                OnLeftClickMouseButton?.Invoke();
            }

            if (Input.GetMouseButtonUp(0))
            {
                OnLeftClickMouseButtonUp?.Invoke();
            }
        
            if (Input.GetMouseButtonDown(1))
            {
                OnRightClickMouseButtonDown?.Invoke(ray);
            }
        }
    }
}