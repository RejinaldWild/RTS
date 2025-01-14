using System;
using static UnityEngine.Object;
using static UnityEngine.Resources;
using _RTSGameProject.Logic.Common.Character.Model;
using UnityEngine;

namespace _RTSGameProject.Logic.Common.Services
{
    public class UnitsFactory
    {
        private UnitsRepository _unitsRepository;
        public UnitsFactory(UnitsRepository unitsRepository)
        {
            _unitsRepository = unitsRepository;
        }

        internal Unit Create(int teamId, Vector3 position)
        {
            Unit resource = Load<Unit>("Prefabs/Unit");
            Unit instance = Instantiate<Unit>(resource, position, Quaternion.identity);
            IDisposable disposable = null;
            
            instance.Construct(teamId, _unitsRepository);
            _unitsRepository.Register(instance);
            instance.Health.OnDie += DisposeUnit;
            
            if (instance.Health.Current <= 0) //?
            {
                DisposeUnit();
            }
            
            _unitsRepository.Register(instance);

            return instance;

            void DisposeUnit()
            {
                instance.Health.OnDie -= DisposeUnit;
                _unitsRepository.Unregister(instance);
                disposable.Dispose();
                Destroy(instance);
            }
        }
    }
}