using _RTSGameProject.Logic.Common.Score.View;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _RTSGameProject.Logic.LoadingAssets.Local
{
    public class ScoreGameUIProvider : LocalAssetLoading
    {
        private readonly Canvas _canvas;
        
        public ScoreGameUIProvider(Canvas canvas)
        {
            _canvas = canvas;
        }
        public async UniTask<ScoreGameUI> Load()
        {
            var asset = await LoadLocalAsset<ScoreGameUI>("ScoreGameUI", _canvas);
            return asset;
        }

        public void Unload()
        {
            UnloadLocalAsset();
        }
    }
}