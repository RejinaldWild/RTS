using _RTSGameProject.Logic.Common.SaveLoad;
using _RTSGameProject.Logic.Common.Services;
using _RTSGameProject.Logic.SDK;
using _RTSGameProject.Logic.SDK.Firebase;
using Zenject;

namespace _RTSGameProject.Logic.Bootstrap
{
    public class EntryPointInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindSaveService();
            BindFirebase();
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

        private void BindFirebase()
        {
            Container
                .Bind<ISDK>().To<FirebaseInitializer>()
                .AsSingle();
            Container
                .Bind<FirebaseEventer>()
                .AsSingle();
        }
    }
}
