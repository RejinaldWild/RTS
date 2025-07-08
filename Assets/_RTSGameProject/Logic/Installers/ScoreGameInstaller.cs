using _RTSGameProject.Logic.Common.Config;
using _RTSGameProject.Logic.Common.SaveLoad;
using _RTSGameProject.Logic.Common.Services;
using _RTSGameProject.Logic.LoadingAssets.Local;
using UnityEngine;
using Zenject;

namespace _RTSGameProject.Logic.Installers
{
    public class ScoreGameInstaller : MonoInstaller
    {
        [SerializeField] private Canvas _canvas;
        
        public override void InstallBindings()
        {
            BindScore();
            BindWinLose();
        }

        private void BindWinLose()
        {
            Container
                .Bind<WinLoseWindowProvider>()
                .AsSingle();
        }

        private void BindScore()
        {
            Container
                .Bind<Canvas>()
                .FromInstance(_canvas)
                .AsSingle();
            Container
                .Bind<ScoreGameUIProvider>()
                .AsSingle();
            Container
                .Bind<ISaveService>().To<SaveService>()
                .AsSingle();
            Container
                .BindInterfacesAndSelfTo<WinLoseGame>()
                .AsSingle();
            Container
                .BindInterfacesAndSelfTo<ScoreGameController>()
                .AsSingle();
        }
    }
}