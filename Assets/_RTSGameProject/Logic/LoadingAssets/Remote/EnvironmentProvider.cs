using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _RTSGameProject.Logic.LoadingAssets.Remote
{
    public class EnvironmentProvider : RemoteAssetLoading
    {
        private Vector3 startPosition = new Vector3(0, 0, 0);
        
        public async UniTask<Terrain> Load()
        {
            var asset = await InstantiateLoadedAssetAsync<Terrain>("TerrainGroundLevel" + SceneManager.GetActiveScene().buildIndex, startPosition);
            return asset;
        }

        public void Unload()
        {
            UnloadRemoteAsset();
        }
    }
}