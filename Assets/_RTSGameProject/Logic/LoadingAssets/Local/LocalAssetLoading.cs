using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _RTSGameProject.Logic.LoadingAssets.Local
{
    public abstract class LocalAssetLoading
    {
        private GameObject _cachedObject;

        public async UniTask<T> LoadLocalAsset<T>(string assetId, Canvas canvas)
        {
            var handle = Addressables.InstantiateAsync(assetId, canvas.transform);
            _cachedObject = await handle.Task;
            
            if (_cachedObject.TryGetComponent(out T assetObject)==false)
            {
                throw new NullReferenceException($"Object {typeof(T)} is null");
            }

            return assetObject;
        }
        
        public void UnloadLocalAsset()
        {
            if(_cachedObject!=null) 
                return;
            
            _cachedObject = null;
        }
    }
}