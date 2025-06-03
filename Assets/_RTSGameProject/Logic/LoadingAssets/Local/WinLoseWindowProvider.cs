using _RTSGameProject.Logic.Common.Services;
using _RTSGameProject.Logic.Common.View;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _RTSGameProject.Logic.LoadingAssets.Local
{
    public class WinLoseWindowProvider : LocalAssetLoading
    {
        private readonly Canvas _canvas;
        private SceneChanger _sceneChanger;
        
        public WinLoseWindowProvider(Canvas canvas, SceneChanger sceneChanger)
        {
            _canvas = canvas;
            _sceneChanger = sceneChanger;
        }
        
        public async UniTask<WinLoseWindow> Load()
        {
            var asset = await LoadLocalAsset<WinLoseWindow>("WinLoseWindow", _canvas);
            asset.Construct(_sceneChanger);
            return asset;
        }

        public void Unload()
        {
            UnloadLocalAsset();
        }
    }
}