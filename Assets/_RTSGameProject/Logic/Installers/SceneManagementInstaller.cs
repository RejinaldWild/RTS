using _RTSGameProject.Logic.Common.Config;
using _RTSGameProject.Logic.Common.Services;
using _RTSGameProject.Logic.LoadingAssets.Remote;
using UnityEngine;
using Zenject;

namespace _RTSGameProject.Logic.Installers
{
    public class SceneManagementInstaller : MonoInstaller
    {
        [SerializeField] private EnemyBuildingsPosConfig _enemyBuildingsPos;
        [SerializeField] private BuildingsPosConfig _buildingsPos;
        
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
                .Bind<BuildingsPosConfig>()
                .FromInstance(_buildingsPos)
                .AsSingle();
            Container
                .Bind<EnemyBuildingsPosConfig>()
                .FromInstance(_enemyBuildingsPos)
                .AsSingle();
            Container
                .Bind<BuildingProvider>()
                .AsSingle();
            Container
                .Bind<BuildingEnemyProvider>()
                .AsSingle();
        }
    }
}