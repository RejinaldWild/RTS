using System;
using static UnityEngine.Object;
using static UnityEngine.Resources;
using _RTSGameProject.Logic.Common.Character.Model;
using _RTSGameProject.Logic.Common.Config;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using Zenject;
using Unit = _RTSGameProject.Logic.Common.Character.Model.Unit;

namespace _RTSGameProject.Logic.Common.Services
{
    public class UnitsFactory : PlaceholderFactory<Unit>
    {
        private readonly UnitsRepository _unitsRepository;
        private readonly HealthBarFactory _healthBarFactory;
        private readonly PauseGame _pauseGame;
        private readonly FirebaseRemoteConfigProvider _firebaseRemoteConfigProvider;
        
        public UnitsFactory(UnitsRepository unitsRepository, 
            HealthBarFactory healthBarFactory, 
            PauseGame pauseGame, 
            FirebaseRemoteConfigProvider firebaseRemoteConfigProvider)
        {
            _unitsRepository = unitsRepository;
            _healthBarFactory = healthBarFactory;
            _pauseGame = pauseGame;
            _firebaseRemoteConfigProvider = firebaseRemoteConfigProvider;
        }

        internal async UniTask<Unit> Create(int teamId, Vector3 position)
        {
            Unit resource = Load<Unit>("Prefabs/Unit");
            Unit instance = Instantiate<Unit>(resource, position, Quaternion.identity);
            if (_firebaseRemoteConfigProvider.UnitConfig.ParamConfigs.ContainsKey("Unit"))
            {
                ParamConfig config =_firebaseRemoteConfigProvider.UnitConfig.ParamConfigs["Unit"];
                instance.Construct(teamId, _unitsRepository, config);
                await _healthBarFactory.Create(instance, instance.GetComponent<Health>());
                _pauseGame.OnPause += instance.OnPaused;
                _pauseGame.OnUnPause += instance.OnUnPaused;
                _unitsRepository.Register(instance);
            };
            
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
        
        internal async UniTask<Unit> CreateExpUnit(int teamId, Vector3 position)
        {
            Unit resource = Load<Unit>("Prefabs/UnitExp");
            Unit instance = Instantiate<Unit>(resource, position, Quaternion.identity);
            if (_firebaseRemoteConfigProvider.UnitConfig.ParamConfigs.ContainsKey("Unit"))
            {
                ParamConfig config =_firebaseRemoteConfigProvider.UnitConfig.ParamConfigs["UnitExp"];
                instance.Construct(teamId, _unitsRepository,config);
                await _healthBarFactory.Create(instance, instance.GetComponent<Health>());
                _pauseGame.OnPause += instance.OnPaused;
                _pauseGame.OnUnPause += instance.OnUnPaused;
                _unitsRepository.Register(instance);
            }
            
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