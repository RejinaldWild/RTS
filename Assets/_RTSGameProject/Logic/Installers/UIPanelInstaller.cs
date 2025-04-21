using _RTSGameProject.Logic.Common.Construction.Model;
using _RTSGameProject.Logic.Common.Construction.View;
using UnityEngine;
using Zenject;

namespace _RTSGameProject.Logic.Installers
{
    public class UIPanelInstaller : MonoInstaller
    {
        [SerializeField] private BuildPanel _buildPanel;
        
        public override void InstallBindings()
        {
            BindBuildUIPanel();
        }

        public void BindBuildUIPanel()
        {
            Container
                .Bind<BuildPanel>()
                .FromInstance(_buildPanel)
                .AsSingle();
            Container
                .Bind<PanelController>()
                .AsSingle();
        }
    }
}