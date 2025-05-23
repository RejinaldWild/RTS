using System;
using _RTSGameProject.Logic.Common.Score.Model;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _RTSGameProject.Logic.LoadingAssets.Local
{
    public abstract class LocalAssetLoading: ILocalAssetLoader
    {
        private GameObject _cachedObject;
        private ScoreGameData _scoreGameData;

        public async UniTask<T> LoadLocalAsset<T>(string assetId, Canvas canvas)
        {
            //AsyncOperationHandle<ScoreGameUI> handles = Addressables.LoadAssetAsync<ScoreGameUI>(assetId);
            var handle = Addressables.InstantiateAsync(assetId, canvas.transform);
            _cachedObject = await handle.Task; // ToUniTask?
            
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
            
            //_cachedObject.SetActive(false);
            //Addressables.ReleaseInstance(_cachedObject);
            _cachedObject = null;
        }
    }
}