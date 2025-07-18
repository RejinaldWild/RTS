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
                .BindInterfacesAndSelfTo<WinLoseActions>()
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