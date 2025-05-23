using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _RTSGameProject.Logic.LoadingAssets.Local
{
    public interface ILocalAssetLoader
    {
        public UniTask<T> LoadLocalAsset<T>(string assetId, Canvas canvas);
        public void UnloadLocalAsset();
    }
}