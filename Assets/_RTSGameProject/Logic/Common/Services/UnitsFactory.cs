using System;
using static UnityEngine.Object;
using static UnityEngine.Resources;
using _RTSGameProject.Logic.Common.Character.Model;
using UniRx;
using UnityEngine;
using Unit = _RTSGameProject.Logic.Common.Character.Model.Unit;

namespace _RTSGameProject.Logic.Common.Services
{
    public class UnitsFactory
    {
        private UnitsRepository _unitsRepository;
        private HealthBarFactory _healthBarFactory;
        private PauseGame _pauseGame;
        public UnitsFactory(UnitsRepository unitsRepository, HealthBarFactory healthBarFactory, PauseGame pauseGame)
        {
            _unitsRepository = unitsRepository;
            _healthBarFactory = healthBarFactory;
            _pauseGame = pauseGame;
        }

        internal Unit Create(int teamId, Vector3 position)
        {
            Unit resource = Load<Unit>("Prefabs/Unit");
            Unit instance = Instantiate<Unit>(resource, position, Quaternion.identity);
            
            instance.Construct(teamId, _unitsRepository);
            _healthBarFactory.Create(instance, instance.GetComponent<Health>());
            _pauseGame.OnPause += instance.OnPaused;
            _pauseGame.OnUnPause += instance.OnUnPaused;
            _unitsRepository.Register(instance);
            
            IDisposable disposable = null;
            disposable = instance.IsAlive.Subscribe(isAlive =>
            {
                if (!isAlive)
                {
                    Dispose();
                }
            });

            return instance;

            void Dispose()
            {
                _unitsRepository.Unregister(instance);
                _pauseGame.OnPause -= instance.OnPaused;
                _pauseGame.OnUnPause -= instance.OnUnPaused;
                Destroy(instance.gameObject);
                disposable.Dispose();
            }

        }
    }
}