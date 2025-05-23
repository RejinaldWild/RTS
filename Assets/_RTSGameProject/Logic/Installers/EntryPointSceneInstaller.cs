using _RTSGameProject.Logic.Bootstrap;
using Zenject;

namespace _RTSGameProject.Logic.Installers
{
    public class EntryPointSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<EntryPointCore>()
                .AsSingle();
        }
    }
}