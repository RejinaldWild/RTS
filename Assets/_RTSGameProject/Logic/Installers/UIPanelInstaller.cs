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
            BuildUIPanelBind();
        }

        public void BuildUIPanelBind()
        {
            Container.Bind<BuildPanel>().FromInstance(_buildPanel).AsSingle();
        }
    }
}