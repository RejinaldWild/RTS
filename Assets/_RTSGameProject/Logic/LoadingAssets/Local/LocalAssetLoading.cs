using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _RTSGameProject.Logic.LoadingAssets.Local
{
    public abstract class LocalAssetLoading
    {
        private GameObject _cachedObject;

        protected async UniTask<T> LoadLocalAsset<T>(string assetId, Canvas canvas)
        {
            var handle = Addressables.InstantiateAsync(assetId, canvas.transform);
            _cachedObject = await handle.ToUniTask();
            
            if (_cachedObject.TryGetComponent(out T assetObject)==false)
            {
                throw new NullReferenceException($"Object {typeof(T)} is null");
            }

            return assetObject;
        }
        
        protected void UnloadLocalAsset()
        {
            if(_cachedObject==null) 
                return;
            
            Addressables.ReleaseInstance(_cachedObject);
            _cachedObject = null;
        }
    }
}