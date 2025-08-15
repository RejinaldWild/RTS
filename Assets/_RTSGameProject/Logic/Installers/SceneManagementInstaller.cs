using _RTSGameProject.Logic.Common.Services;
using _RTSGameProject.Logic.LoadingAssets.Remote;
using Zenject;

namespace _RTSGameProject.Logic.Installers
{
    public class SceneManagementInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindMainMenuSceneChanger();
            BindEnviromentProvider();
            BindBuildingProvider();
            BindUnitProvider();
            BindUnitExpProvider();
        }

        private void BindUnitProvider()
        {
            Container
                .Bind<UnitProvider>()
                .AsSingle();
        }

        private void BindUnitExpProvider()
        {
            Container
                .Bind<UnitExpProvider>()
                .AsSingle();
        }

        private void BindMainMenuSceneChanger()
        {
            Container
                .BindInterfacesAndSelfTo<SceneChanger>()
                .AsSingle();
        }

        private void BindEnviromentProvider()
        {
            Container
                .Bind<EnvironmentProvider>()
                .AsSingle();
        }

        private void BindBuildingProvider()
        {
            Container
                .Bind<BuildingProvider>()
                .AsSingle();
            Container
                .Bind<BuildingEnemyProvider>()
                .AsSingle();
        }
    }
}