using System.Collections.Generic;
using _RTSGameProject.Logic.Analytic;
using _RTSGameProject.Logic.Common.AI;
using _RTSGameProject.Logic.Common.Config;
using UnityEngine;
using UnityEngine.AI;

namespace _RTSGameProject.Logic.Common.Services
{
    public class Spawner
    {
        private UnitsRepository _unitsRepository;
        private AiFactory _aiFactory;
        private IAnalyticService _analyticService;
        private IVFX _vfxService;
        private int _tryFindPoint = 30;
        private int _spawnUnitCount = 0;
        private int _spawnExpUnitCount = 0;
        private float _radius = 0.5f;
        private float _maxDistance = 1f;

        public Spawner(AiFactory aiFactory, IAnalyticService analyticService, IVFX vfxService)
        {
            _aiFactory = aiFactory;
            _analyticService = analyticService;
            _vfxService = vfxService;
        }   
        
        public void Spawn(int teamId, Vector3 position)
        {
            Vector3 spawnPosition = FindSpawnPosition(position, _radius);
            _vfxService.ShowEffect(VFXType.CREATE, GetSpawnEffectPosition(spawnPosition));
            if (teamId == 0)
            {
                _spawnUnitCount++;
                _analyticService.SendBuildUnit(_spawnUnitCount);
                _aiFactory.Create(teamId, spawnPosition);
            }
            if(teamId == 1)
                _aiFactory.Create(teamId, spawnPosition);
        }

        public void Spawn(int teamId, List<Transform> positions)
        {
            Vector3 spawnPosition = FindSpawnPosition(GetRandomPositionFromBuilding(positions), _radius);
            _vfxService.ShowEffect(VFXType.CREATE, GetSpawnEffectPosition(spawnPosition));
            if (teamId == 0)
            {
                _spawnUnitCount++;
                _analyticService.SendBuildUnit(_spawnUnitCount);
                _aiFactory.Create(teamId, spawnPosition);
            }
            if(teamId == 1)
                _aiFactory.Create(teamId, spawnPosition);
        }


        public void SpawnExpensiveUnit(int team, Vector3 position)
        {
            Vector3 spawnPosition = FindSpawnPosition(position, _radius);
            _vfxService.ShowEffect(VFXType.CREATE, GetSpawnEffectPosition(spawnPosition));
            if (team == 0)
            {
                _spawnExpUnitCount++;
                _analyticService.SendBuildExpensiveUnit(_spawnExpUnitCount);
                _aiFactory.CreateExpUnit(team, spawnPosition);
            }
        }
        
        private Vector3 GetSpawnEffectPosition(Vector3 spawnPosition)
        {
            var spawnEffectPosition = spawnPosition;
            spawnEffectPosition.y = 0f;
            return spawnEffectPosition;
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

        private Vector3 GetRandomPositionFromBuilding(List<Transform> positions)
        {
            return positions[Random.Range(0, positions.Count)].position;
        }
    }
}