using _RTSGameProject.Logic.Common.Config;
using _RTSGameProject.Logic.Common.SaveLoad;
using _RTSGameProject.Logic.Common.Score.Model;
using _RTSGameProject.Logic.Common.Score.View;
using _RTSGameProject.Logic.Common.Services;
using _RTSGameProject.Logic.Common.View;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace _RTSGameProject.Logic.Installers
{
    public class ScoreGameInstaller : MonoInstaller
    {
        [SerializeField] private ScoreGameUI _scoreGameUI;
        [SerializeField] private WinLoseConfig _winLoseConfig;
        
        public override void InstallBindings()
        {
            BindWinLose();
            ScoreBind();
        }

        private void BindWinLose()
        {
            Container
                .Bind<WinLoseConfig>()
                .FromInstance(_winLoseConfig)
                .AsSingle();
        }

        private void ScoreBind()
        {
            Container
                .Bind<ScoreGameUI>()
                .FromInstance(_scoreGameUI);
            Container
                .Bind<ScoreGameData>()
                .AsSingle();
            Container
                .Bind<SaveSystem>()
                .AsSingle();
            Container
                .Bind<WinLoseGame>()
                .AsSingle();
            Container
                .Bind<ScoreGameController>()
                .AsSingle();
        }
    }
}