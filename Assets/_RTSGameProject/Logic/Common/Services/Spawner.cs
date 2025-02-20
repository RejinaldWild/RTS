using _RTSGameProject.Logic.Common.AI;
using UnityEngine;
using UnityEngine.AI;

namespace _RTSGameProject.Logic.Common.Services
{
    public class Spawner
    {
        private UnitsRepository _unitsRepository;
        private AiFactory _aiFactory;
        private int _tryFindPoint = 30;
        private float _radius = 0.5f;
        private float _maxDistance = 1f;

        public Spawner(AiFactory aiFactory)
        {
            _aiFactory = aiFactory;
        }   
        
        public void Spawn(int teamId, Vector3 position)
        {
            Vector3 spawnPosition = FindSpawnPosition(position, _radius);
            if (teamId == 0)
            {
                _aiFactory.Create(teamId, spawnPosition);
            }
            if(teamId == 1)
                _aiFactory.Create(teamId, spawnPosition);
        }

        private Vector3 FindSpawnPosition(Vector3 center, float radius)
        {
            for (int i = 0; i < _tryFindPoint; i++)
            {
                Vector3 randomPoint = center + Random.insideUnitSphere * radius;
                if(NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, _maxDistance, NavMesh.AllAreas))
                    return hit.position;
            }
            return Vector3.zero;
        }
    }
}