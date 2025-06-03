using _RTSGameProject.Logic.Common.View;
using Cysharp.Threading.Tasks;
using UnityEngine;
using _RTSGameProject.Logic.Common.Character.Model;

namespace _RTSGameProject.Logic.LoadingAssets.Local
{
    public class HealthBarSliderProvider : LocalAssetLoading
    {
        public async UniTask<HealthView> Load(Unit unit)
        {
            var canvasComponent = unit.GetComponentInChildren<Canvas>();
            var asset = await LoadLocalAsset<HealthView>("HealthBarSlider", canvasComponent);
            return asset;
        }

        public void Unload()
        {
            UnloadLocalAsset();
        }
    }
}