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
        private const string BUILD_ENEMY_NAME = "BuildingEnemy(Clone)";
        
        private int _sceneIndex;
        private DiContainer _container;
        private Vector3[] _enemyBuildingScenePositions;
        
        public BuildingEnemyProvider(DiContainer container)
        {
            _container = container;
        }

        public void Initialize(EnemyBuildingsPosConfig enemyBuildingsPosConfig)
        {
            _enemyBuildingScenePositions = enemyBuildingsPosConfig.BuildingPositionScene;
        }
        
        public async UniTask<HouseBuilding[]> Load()
        {
            List<HouseBuilding> buildings = new List<HouseBuilding>();
            _sceneIndex = SceneManager.GetActiveScene().buildIndex;
            if (_sceneIndex == (int)SceneNameAndNumber.SceneName01)
            {
                for(int i = 0; i < _enemyBuildingScenePositions.Length; i++)
                {
                    HouseBuilding building = await InstantiateLoadedAssetAsync<HouseBuilding>("BuildingEnemy" , _enemyBuildingScenePositions[i]);
                    if (building.name == BUILD_ENEMY_NAME)
                    {
                        _container.InjectGameObject(building.gameObject);
                        buildings.Add(building);
                    }
                }
            }

            if (_sceneIndex == (int)SceneNameAndNumber.SceneName02)
            {
                for(int i = 0; i < _enemyBuildingScenePositions.Length; i++)
                {
                    HouseBuilding building = await InstantiateLoadedAssetAsync<HouseBuilding>("BuildingEnemy" , _enemyBuildingScenePositions[i]);
                    if (building.name == BUILD_ENEMY_NAME)
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