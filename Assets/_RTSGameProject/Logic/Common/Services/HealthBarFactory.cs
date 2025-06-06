using System;
using _RTSGameProject.Logic.Common.Character.Model;
using _RTSGameProject.Logic.Common.View;
using _RTSGameProject.Logic.LoadingAssets.Local;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using Zenject;
using static UnityEngine.Object;
using Unit = _RTSGameProject.Logic.Common.Character.Model.Unit;

namespace _RTSGameProject.Logic.Common.Services
{
    public class HealthBarFactory: PlaceholderFactory<HealthView>
    {
        private HealthBarRepository _healthBarRepository;
        private Camera _mainCamera;
        private HealthBarSliderProvider _healthBarSliderProvider;
        
        [Inject]
        public HealthBarFactory(HealthBarRepository healthBarRepository, Camera mainCamera, HealthBarSliderProvider healthBarSliderProvider)
        {
            _healthBarRepository = healthBarRepository;
            _mainCamera = mainCamera;
            _healthBarSliderProvider = healthBarSliderProvider;
        }

        public async UniTask Create(Unit unit, Health healthModel)
        {
            HealthViewModel healthViewModel = new HealthViewModel(healthModel);
            HealthView instance = await _healthBarSliderProvider.Load(unit);
            instance.Construct(healthViewModel, _mainCamera);
            _healthBarRepository.Register(instance);

            IDisposable disposable = null;
            
            disposable = healthModel.IsAlive.Subscribe(isAlive =>
            {
                if (!isAlive)
                {
                    Unregister();
                }
            });
            
            void Unregister()
            {
                _healthBarSliderProvider.Unload();
                Destroy(instance.gameObject);
                _healthBarRepository.Unregister(instance);
                disposable.Dispose();
            }
        }

    }
}
