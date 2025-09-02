using System;
using static UnityEngine.Object;
using _RTSGameProject.Logic.Common.Character.Model;
using _RTSGameProject.Logic.Common.Config;
using _RTSGameProject.Logic.Common.Services.SoundFX;
using _RTSGameProject.Logic.LoadingAssets.Remote;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using Zenject;
using Unit = _RTSGameProject.Logic.Common.Character.Model.Unit;

namespace _RTSGameProject.Logic.Common.Services
{
    public class UnitsFactory : PlaceholderFactory<Unit>
    {
        public const string UNIT = "Unit";
        public const string UNIT_EXP = "UnitExp";
        
        private readonly UnitsRepository _unitsRepository;
        private readonly HealthBarFactory _healthBarFactory;
        private readonly PauseGame _pauseGame;
        private readonly IRemoteConfigProvider _remoteConfigProvider;
        private readonly UnitProvider _unitProvider;
        private readonly UnitExpProvider _unitExpProvider;

        private Unit _unit;
        private Unit _unitExp;
        private IAudio _audioService;
        private IVFX _vfxService;
        
        public UnitsFactory(UnitsRepository unitsRepository, 
            HealthBarFactory healthBarFactory, 
            PauseGame pauseGame, 
            UnitProvider unitProvider,
            UnitExpProvider unitExpProvider,
            IRemoteConfigProvider remoteConfigProvider,
            IAudio audioService,
            IVFX vfxService)
        {
            _unitsRepository = unitsRepository;
            _healthBarFactory = healthBarFactory;
            _pauseGame = pauseGame;
            _remoteConfigProvider = remoteConfigProvider;
            _unitProvider = unitProvider;
            _unitExpProvider = unitExpProvider;
            _audioService = audioService;
            _vfxService = vfxService;
        }

        public async UniTask Initialize()
        {
            var unitTask = _unitProvider.LoadRemoteAsset<Unit>(UNIT);
            var unitExpTask = _unitExpProvider.LoadRemoteAsset<Unit>(UNIT_EXP);

            var results = await UniTask.WhenAll(unitTask, unitExpTask);

            _unit = results.Item1;
            _unitExp = results.Item2;
        }
        
        internal async UniTask<Unit> Create(int teamId, Vector3 position)
        {
            Unit instance = Instantiate<Unit>(_unit, position, Quaternion.identity);
            if (_remoteConfigProvider.UnitConfig.ParamConfigs.ContainsKey(UNIT))
            {
                ParamConfig config =_remoteConfigProvider.UnitConfig.ParamConfigs[UNIT];
                instance.Construct(teamId, _unitsRepository, config, _audioService, _vfxService);
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
                _unitProvider.Unload();
                disposable.Dispose();
            }

        }
        
        internal async UniTask<Unit> CreateExpUnit(int teamId, Vector3 position)
        {
            Unit instance = Instantiate<Unit>(_unitExp, position, Quaternion.identity);
            if (_remoteConfigProvider.UnitConfig.ParamConfigs.ContainsKey(UNIT_EXP))
            {
                ParamConfig config =_remoteConfigProvider.UnitConfig.ParamConfigs[UNIT_EXP];
                instance.Construct(teamId, _unitsRepository,config, _audioService, _vfxService);
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
                _unitExpProvider.Unload();
                disposable.Dispose();
            }

        }
    }
}