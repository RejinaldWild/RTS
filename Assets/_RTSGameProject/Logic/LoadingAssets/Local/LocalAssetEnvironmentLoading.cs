using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _RTSGameProject.Logic.LoadingAssets.Local
{
    public abstract class LocalAssetEnvironmentLoading
    {
        private GameObject _cachedObject;

        public async UniTask<T> LoadLocalAsset<T>(string assetId)
        {
            var handle = Addressables.InstantiateAsync(assetId);
            _cachedObject = await handle.Task;
            
            if (_cachedObject.TryGetComponent(out T assetObject)==false)
            {
                throw new NullReferenceException($"Object {typeof(T)} is null");
            }

            return assetObject;
        }
        
        public void UnloadLocalAsset()
        {
            if(_cachedObject==null)
                return;
            
            Addressables.ReleaseInstance(_cachedObject);
            _cachedObject = null;
        }
    }
}