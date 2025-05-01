using _RTSGameProject.Logic.Common.SaveLoad;
using _RTSGameProject.Logic.Common.Services;
using Zenject;

namespace _RTSGameProject.Logic.Installers
{
    public class SaveLoadInstaller : MonoInstaller
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
                .Bind<IKeyProvider>()
                .To<MapKeyProvider>()
                .AsSingle();
            Container
                .Bind<SaveDataBase>()
                .AsSingle();
        }
    }
}