using _RTSGameProject.Logic.Common.Services;
using Zenject;

namespace _RTSGameProject.Logic.Bootstrap
{
    public class EntryPointInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SaveSystemBind();
        }

        private void SaveSystemBind()
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
