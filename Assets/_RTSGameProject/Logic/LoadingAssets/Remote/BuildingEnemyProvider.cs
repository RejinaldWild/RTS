using System.Collections.Generic;
using _RTSGameProject.Logic.Common.Config;
using _RTSGameProject.Logic.Common.Construction.Model;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace _RTSGameProject.Logic.LoadingAssets.Remote
{
    public class BuildingEnemyProvider : RemoteAssetLoading
    {
        private int _sceneIndex;
        private DiContainer _container;
        private Vector3[] _buildingScenePositions;
        
        public BuildingEnemyProvider(DiContainer container)
        {
            _container = container;
        }

        public void Initialize(EnemyBuildingsPosConfig enemyBuildingsPosConfig)
        {
            _buildingScenePositions = enemyBuildingsPosConfig.BuildingPositionScene;
        }
        
        public async UniTask<HouseBuilding[]> Load()
        {
            List<HouseBuilding> buildings = new List<HouseBuilding>();
            _sceneIndex = SceneManager.GetActiveScene().buildIndex;
            if (_sceneIndex == 1)
            {
                for(int i = 0; i < _buildingScenePositions.Length; i++)
                {
                    HouseBuilding building = await InstantiateLoadedAssetAsync<HouseBuilding>("BuildingEnemy" , _buildingScenePositions[i]);
                    if (building.name == "BuildingEnemy(Clone)")
                    {
                        _container.InjectGameObject(building.gameObject);
                        buildings.Add(building);
                    }
                }
            }

            if (_sceneIndex == 2)
            {
                for(int i = 0; i < _buildingScenePositions.Length; i++)
                {
                    HouseBuilding building = await InstantiateLoadedAssetAsync<HouseBuilding>("BuildingEnemy" , _buildingScenePositions[i]);
                    if (building.name == "BuildingEnemy(Clone)")
                    {
                        _container.InjectGameObject(building.gameObject);
                        buildings.Add(building);
                    }
                }
            }
            return buildings.ToArray();
        }
        
        public void Unload()
        {
            UnloadRemoteAsset();
        }
    }
}