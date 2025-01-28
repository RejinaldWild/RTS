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
            
            instance.Construct(teamId, _unitsRepository);
            _unitsRepository.Register(instance);
            instance.Health.OnDie += DisposeUnit;

            return instance;

            void DisposeUnit()
            {
                instance.Health.OnDie -= DisposeUnit;
                _unitsRepository.Unregister(instance);
                Destroy(instance.gameObject);
            }
        }
    }
}