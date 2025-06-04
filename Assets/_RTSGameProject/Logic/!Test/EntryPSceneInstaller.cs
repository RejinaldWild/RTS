using _RTSGameProject.Logic.Common.SaveLoad;
using _RTSGameProject.Logic.Common.Score.Model;
using _RTSGameProject.Logic.Common.Services;
using Zenject;

namespace _RTSGameProject.Logic.Installers
{
    public class EntryPSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<ISerializer>()
                .To<JsonConverter>()
                .AsSingle();
            Container
                .Bind<IDataStorage>()
                .To<PlayerPrefsDataStorage>()
                .AsSingle();
            Container
                .Bind<SaveDataBase>()
                .AsSingle();
            Container
                .Bind<IKeyProvider>()
                .To<KeyProvider>()
                .AsSingle();
            Container
                .Bind<ScoreGameData>()
                .AsSingle();
            Container
                .Bind<SaveScoreService>()
                .AsSingle();
            Container
                .BindInterfacesAndSelfTo<EntryP>()
                .AsSingle();
        }
    }
}