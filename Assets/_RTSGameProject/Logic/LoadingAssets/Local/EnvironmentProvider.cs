using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _RTSGameProject.Logic.LoadingAssets.Local
{
    public class EnvironmentProvider : LocalAssetEnvironmentLoading
    {
        public async UniTask<Terrain> Load()
        {
            var asset = await LoadLocalAsset<Terrain>("TerrainGroundLevel" + SceneManager.GetActiveScene().buildIndex);
            return asset;
        }

        public void Unload()
        {
            UnloadLocalAsset();
        }
    }
}