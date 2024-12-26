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
            
            instance.Construct(teamId);

            IDisposable disposable = null;
            if (instance.Health.Current <= 0) //?
            {
                DisposeUnit();
            }
            
            _unitsRepository.Register(instance);

            return instance;

            void DisposeUnit()
            {
                _unitsRepository.Unregister(instance);
                Destroy(instance);
                disposable.Dispose();
            }

        }
    }
}