using System.Collections.Generic;
using _RTSGameProject.Logic.Common.Construction.Model;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace _RTSGameProject.Logic.LoadingAssets.Remote
{
    public class BuildingProvider : RemoteAssetLoading
    {
        private int _sceneIndex;
        private DiContainer _container;
        
        private readonly Vector3[] BuildingPositionScene1 = 
        {
            new (23.59f, 0.5f, 30.48f),
            new (25.57f,0.5f,32.63f)
        };
        
        private readonly Vector3[] BuildingPositionScene2 = 
        {
            new (25.57f,0.5f,32.63f),
            new (23.59f,0.5f,30.48f)
        };

        public BuildingProvider(DiContainer container)
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
                    HouseBuilding building = await InstantiateLoadedAssetAsync<HouseBuilding>("Building" , BuildingPositionScene1[i]);
                    if (building.name == "Building(Clone)")
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
                    HouseBuilding building = await InstantiateLoadedAssetAsync<HouseBuilding>("Building" , BuildingPositionScene2[i]);
                    if (building.name == "Building(Clone)")
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