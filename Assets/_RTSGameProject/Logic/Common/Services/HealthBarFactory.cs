using System;
using _RTSGameProject.Logic.Common.Character.Model;
using _RTSGameProject.Logic.Common.View;
using UniRx;
using static UnityEngine.Object;
using Unit = _RTSGameProject.Logic.Common.Character.Model.Unit;

namespace _RTSGameProject.Logic.Common.Services
{
    public class HealthBarFactory
    {
        private HealthBarRepository _healthBarRepository;
        
        public HealthBarFactory(HealthBarRepository healthBarRepository)
        {
            _healthBarRepository = healthBarRepository;
        }

        public void Create(Unit unit, Health healthModel)
        {
            HealthViewModel healthViewModel = new HealthViewModel(healthModel);
            HealthView instance = unit.GetComponentInChildren<HealthView>();
            instance.Construct(healthViewModel);
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
                Destroy(instance.gameObject);
                _healthBarRepository.Unregister(instance);
                disposable.Dispose();
            }
        }

    }
}
