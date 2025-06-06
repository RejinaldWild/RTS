using _RTSGameProject.Logic.Common.Construction.Model;
using _RTSGameProject.Logic.Common.Construction.View;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _RTSGameProject.Logic.LoadingAssets.Local
{
    public class ProductionPanelProvider : LocalAssetLoading
    {
        private readonly Canvas _canvas;
        private readonly PanelController _panelController;
        
        public ProductionPanelProvider(Canvas canvas, PanelController panelController)
        {
            _canvas = canvas.GetComponentsInChildren<Canvas>()[2];//?
            _panelController = panelController;
        }
        
        public async UniTask<ProductionPanel> Load()
        {
            var asset = await LoadLocalAsset<ProductionPanel>("ProductionPanel", _canvas);
            _panelController.Initialize(asset);
            return asset;
        }

        public void Unload()
        {
            UnloadLocalAsset();
        }
    }
}