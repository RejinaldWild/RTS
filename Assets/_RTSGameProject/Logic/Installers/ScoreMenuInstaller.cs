using _RTSGameProject.Logic.Common.SaveLoad;
using _RTSGameProject.Logic.Common.Score.Model;
using _RTSGameProject.Logic.Common.Score.View;
using _RTSGameProject.Logic.Common.Services;
using UnityEngine;
using Zenject;

namespace _RTSGameProject.Logic.Installers
{
    public class ScoreMenuInstaller : MonoInstaller
    {
        [SerializeField] private ScoreMenuUI _scoreMenuUI;
        
        public override void InstallBindings()
        {
            Container
                .Bind<ScoreMenuUI>()
                .FromInstance(_scoreMenuUI);
            Container
                .Bind<ScoreGameData>()
                .AsSingle();
            Container
                .Bind<SaveSystem>()
                .AsSingle();
            Container
                .BindInterfacesAndSelfTo<ScoreMenuController>()
                .AsSingle();
        }
    }
}