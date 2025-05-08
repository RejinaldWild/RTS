using _RTSGameProject.Logic.Common.Services;
using UniRx;
using UnityEngine;
using UnityEngine.UI;


namespace _RTSGameProject.Logic.Common.View
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private Slider _healthBarSlider;
        
        private UnityEngine.Camera MainCamera;
        private HealthViewModel _viewModel;
        
        public void Construct(HealthViewModel viewModel)
        {
            _viewModel = viewModel;
        }
        
        private void Start()
        {
            MainCamera = UnityEngine.Camera.main;
            Subscribe();
        }

        private void Subscribe()
        {
            this.ObserveEveryValueChanged(x => x.transform.position)
                .Subscribe(rotation =>
                {
                    LookAtCamera(transform.position);
                });
            
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
            Vector3 directionToLook = MainCamera.transform.position - position;
            directionToLook.y = 0;
            transform.LookAt(directionToLook);
        }
    }
}
