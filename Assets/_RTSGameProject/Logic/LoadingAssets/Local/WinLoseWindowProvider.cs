using _RTSGameProject.Logic.Common.Services;
using _RTSGameProject.Logic.Common.View;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _RTSGameProject.Logic.LoadingAssets.Local
{
    public class WinLoseWindowProvider : LocalAssetLoading
    {
        private readonly Canvas _canvas;
        private WinLoseGame _winLoseGame;
        
        public WinLoseWindowProvider(Canvas canvas)
        {
            _canvas = canvas;
        }
        
        public async UniTask<WinLoseWindow> Load()
        {
            return await LoadLocalAsset<WinLoseWindow>("WinLoseWindow", _canvas);;
        }

        public void Unload()
        {
            UnloadLocalAsset();
        }
    }
}