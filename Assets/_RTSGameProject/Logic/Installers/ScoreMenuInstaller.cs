using _RTSGameProject.Logic.Bootstrap;
using _RTSGameProject.Logic.Common.Score.View;
using _RTSGameProject.Logic.Common.Services;
using _RTSGameProject.Logic.LoadingAssets.Local;
using UnityEngine;
using Zenject;

namespace _RTSGameProject.Logic.Installers
{
    public class ScoreMenuInstaller : MonoInstaller
    {
        [SerializeField] private ScoreMenuUI _scoreMenuUI;
        
        public override void InstallBindings()
        {
            BindMainMenuService();
            BindScoreMenu();
            BindMainMenuSceneChanger();
            BindEnvironmentProvider();
        }

        private void BindMainMenuService()
        {
            Container
                .BindInterfacesAndSelfTo<MainMenuService>()
                .AsSingle();
        }

        private void BindScoreMenu()
        {
            Container
                .Bind<ScoreMenuUI>()
                .FromInstance(_scoreMenuUI);
            Container
                .BindInterfacesAndSelfTo<ScoreMenuController>()
                .AsSingle();
        }

        private void BindMainMenuSceneChanger()
        {
            Container
                .BindInterfacesAndSelfTo<SceneChanger>()
                .AsSingle();
        }

        private void BindEnvironmentProvider()
        {
            Container
                .Bind<EnvironmentProvider>()
                .AsSingle();
        }
    }
}