using _RTSGameProject.Logic.Common.Services;
using _RTSGameProject.Logic.LoadingAssets.Local;
using Zenject;

namespace _RTSGameProject.Logic.Installers
{
    public class SceneManagmentInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindSceneChanger();
            BindEnviromentProvider();
        }

        private void BindSceneChanger()
        {
            Container
                .Bind<SceneChanger>()
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