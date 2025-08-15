using System.Collections.Generic;
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
        
        private readonly Vector3[] BuildingPositionScene1 = 
        {
            new (35.55f,0.5f,21.51f)
        };
        
        private readonly Vector3[] BuildingPositionScene2 = 
        {
            new (35.55f,0.5f,21.51f)
        };

        public BuildingEnemyProvider(DiContainer container)
        {
            _container = container;
        }
        
        public async UniTask<HouseBuilding[]> Load()
        {
            List<HouseBuilding> buildings = new List<HouseBuilding>();
            _sceneIndex = SceneManager.GetActiveScene().buildIndex;
            if (_sceneIndex == 1)
            {
                for(int i = 0; i < BuildingPositionScene1.Length; i++)
                {
                    HouseBuilding building = await InstantiateLoadedAssetAsync<HouseBuilding>("BuildingEnemy" , BuildingPositionScene1[i]);
                    if (building.name == "BuildingEnemy(Clone)")
                    {
                        _container.InjectGameObject(building.gameObject);
                        buildings.Add(building);
                    }
                }
            }

            if (_sceneIndex == 2)
            {
                for(int i = 0; i < BuildingPositionScene2.Length; i++)
                {
                    HouseBuilding building = await InstantiateLoadedAssetAsync<HouseBuilding>("BuildingEnemy" , BuildingPositionScene2[i]);
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