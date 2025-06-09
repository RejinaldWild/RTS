using _RTSGameProject.Logic.Common.SaveLoad;
using _RTSGameProject.Logic.Common.Score.Model;
using _RTSGameProject.Logic.Common.Services;
using Zenject;

namespace _RTSGameProject.Logic.Bootstrap
{
    public class EntryPointInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindSaveService();
        }
        
        private void BindSaveService()
        {
            Container
                .Bind<PlayerPrefsDataStorage>()
                .AsSingle();
            Container
                .Bind<JsonConverter>()
                .AsSingle();
        }
    }
}
