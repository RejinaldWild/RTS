using static UnityEngine.Object;
using static UnityEngine.Resources;
using _RTSGameProject.Logic.Common.Character.Model;
using UnityEngine;

namespace _RTSGameProject.Logic.Common.Services
{
    public class UnitsFactory
    {
        private UnitsRepository _unitsRepository;
        private PauseGame _pauseGame;
        public UnitsFactory(UnitsRepository unitsRepository, PauseGame pauseGame)
        {
            _unitsRepository = unitsRepository;
            _pauseGame = pauseGame;
        }

        internal Unit Create(int teamId, Vector3 position)
        {
            Unit resource = Load<Unit>("Prefabs/Unit");
            Unit instance = Instantiate<Unit>(resource, position, Quaternion.identity);
            
            instance.Construct(teamId, _unitsRepository);
            _pauseGame.OnPause += instance.OnPaused;
            _pauseGame.OnUnPause += instance.OnUnPaused;
            _unitsRepository.Register(instance);
            instance.Health.OnDie += DisposeUnit;

            return instance;

            void DisposeUnit()
            {
                instance.Health.OnDie -= DisposeUnit;
                _unitsRepository.Unregister(instance);
                _pauseGame.OnPause -= instance.OnPaused;
                _pauseGame.OnUnPause -= instance.OnUnPaused;
                Destroy(instance.gameObject);
            }
        }
    }
}