using _RTSGameProject.Logic.Common.SaveLoad;
using _RTSGameProject.Logic.Common.Services;
using Zenject;

namespace _RTSGameProject.Logic.Bootstrap
{
    public class EntryPointInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindSaveSystem();
        }
        
        private void BindSaveSystem()
        {
            Container
                .Bind<PlayerPrefsDataStorage>()
                .AsSingle()
                .NonLazy();
            Container
                .Bind<JsonConverter>()
                .AsSingle()
                .NonLazy();
        }
    }
}
