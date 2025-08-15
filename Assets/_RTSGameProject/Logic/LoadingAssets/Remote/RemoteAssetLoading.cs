using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace _RTSGameProject.Logic.LoadingAssets.Remote
{
    public abstract class RemoteAssetLoading
    {
        private GameObject _cachedObject;

        protected async UniTask<T> InstantiateLoadedAssetAsync<T>(string assetId, Vector3 position)
        {
            var handle = Addressables.InstantiateAsync(assetId, position, Quaternion.identity);
            _cachedObject = await handle.ToUniTask();
            
            if (_cachedObject.TryGetComponent(out T assetObject)==false)
            {
                throw new NullReferenceException($"Object {typeof(T)} is null");
            }

            return assetObject;
        }
        
        public async UniTask<T> LoadRemoteAsset<T>(string assetId)
        {
            var handle = Addressables.LoadAssetAsync<GameObject>(assetId);
            GameObject cacheLoadedObject = await handle.ToUniTask();
            
            if (cacheLoadedObject==null)
            {
                throw new NullReferenceException($"Object {typeof(T)} is null");
            }

            if (cacheLoadedObject.GetComponent<T>() == null)
            {
                throw new NullReferenceException($"Object {cacheLoadedObject} does not have a component {typeof(T)}");
            }
            
            return cacheLoadedObject.GetComponent<T>();
        }
        
        protected void UnloadRemoteAsset()
        {
            if(_cachedObject==null)
                return;
            
            Addressables.ReleaseInstance(_cachedObject);
            _cachedObject = null;
        }
    }
}