using _RTSGameProject.Logic.Common.Construction.Model;
using _RTSGameProject.Logic.LoadingAssets.Local;
using UnityEngine;
using Zenject;

namespace _RTSGameProject.Logic.Installers
{
    public class UIPanelInstaller : MonoInstaller
    {
        [SerializeField] private Canvas _buildingUIGroupCanvas;
        
        public override void InstallBindings()
        {
            BindBuildUIPanel();
        }

        public void BindBuildUIPanel()
        {
            Container
                .Bind<PanelController>()
                .AsSingle();
            Container
                .Bind<ProductionPanelProvider>()
                .AsSingle();
        }
    }
}