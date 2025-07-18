using _RTSGameProject.Logic.Common.Services;
using _RTSGameProject.Logic.LoadingAssets.Local;
using Zenject;

namespace _RTSGameProject.Logic.Installers
{
    public class SceneManagementInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindMainMenuSceneChanger();
            BindEnviromentProvider();
        }

        private void BindMainMenuSceneChanger()
        {
            Container
                .BindInterfacesAndSelfTo<WindowSceneChanger>()
                .AsSingle();
        }

        private void BindEnviromentProvider()
        {
            Container
                .Bind<EnvironmentProvider>()
                .AsSingle();
        }
    }
}