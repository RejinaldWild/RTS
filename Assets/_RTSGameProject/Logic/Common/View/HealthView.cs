using System;
using _RTSGameProject.Logic.Common.Services;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;


namespace _RTSGameProject.Logic.Common.View
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private Slider _healthBarSlider;
        
        private Camera _mainCamera;
        private HealthViewModel _viewModel;
        
        public void Construct(HealthViewModel viewModel, Camera mainCamera)
        {
            _mainCamera = mainCamera;
            _viewModel = viewModel;
        }
        
        private void Start()
        {
            Subscribe();
        }

        private void Subscribe()
        {
            this.ObserveEveryValueChanged(x => x.transform.position)
                .Subscribe(rotation => { LookAtCamera(transform.position);})
                .AddTo(this);
            
            _viewModel.HeathRelative
                .Subscribe(Draw)
                .AddTo(this);
        }
        
        private void Draw(float value)
        {
            _healthBarSlider.value = value;
        }

        private void LookAtCamera(Vector3 position)
        {
            Vector3 directionToLook = _mainCamera.transform.position - position;
            directionToLook.y = 0;
            transform.LookAt(directionToLook);
        }
    }
}
