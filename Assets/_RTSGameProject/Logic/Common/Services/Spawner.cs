using _RTSGameProject.Logic.Common.AI;
using _RTSGameProject.Logic.SDK;
using UnityEngine;
using UnityEngine.AI;

namespace _RTSGameProject.Logic.Common.Services
{
    public class Spawner
    {
        private UnitsRepository _unitsRepository;
        private AiFactory _aiFactory;
        private ISDK _analyticService;
        private int _tryFindPoint = 30;
        private int _spawnUnitCount = 0;
        private int _spawnExpUnitCount = 0;
        private float _radius = 0.5f;
        private float _maxDistance = 1f;

        public Spawner(AiFactory aiFactory, ISDK analyticService)
        {
            _aiFactory = aiFactory;
            _analyticService = analyticService;
        }   
        
        public void Spawn(int teamId, Vector3 position)
        {
            Vector3 spawnPosition = FindSpawnPosition(position, _radius);
            if (teamId == 0)
            {
                _spawnUnitCount++;
                _analyticService.BuiltUnit(_spawnUnitCount);
                _aiFactory.Create(teamId, spawnPosition);
            }
            if(teamId == 1)
                _aiFactory.Create(teamId, spawnPosition);
        }

        public void SpawnExpensiveUnit(int team, Vector3 position)
        {
            Vector3 spawnPosition = FindSpawnPosition(position, _radius);
            if (team == 0)
            {
                _spawnExpUnitCount++;
                _analyticService.BuiltExpensiveUnit(_spawnExpUnitCount);
                _aiFactory.CreateExpUnit(team, spawnPosition);
            }
        }
        
        private Vector3 FindSpawnPosition(Vector3 center, float radius)
        {
            for (int i = 0; i < _tryFindPoint; i++)
            {
                Vector3 randomPoint = center + Random.insideUnitSphere * radius;
                if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, _maxDistance, NavMesh.AllAreas))
                {
                    return new Vector3(hit.position.x, 1f, hit.position.z);
                }
            }
            return Vector3.zero;
        }
    }
}