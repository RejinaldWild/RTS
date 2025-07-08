using _RTSGameProject.Logic.Ads.UnityAds;
using _RTSGameProject.Logic.Analytic.Firebase;
using _RTSGameProject.Logic.Common.Config;
using _RTSGameProject.Logic.Common.SaveLoad;
using _RTSGameProject.Logic.Common.Services;
using Zenject;

namespace _RTSGameProject.Logic.Bootstrap
{
    public class EntryPointInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindSaveService();
            BindFirebase();
            BindAds();
        }

        private void BindAds()
        {
            Container
                .Bind<UnityAdsInterstitial>()
                .AsSingle();
            Container
                .Bind<UnityAdsRewarded>()
                .AsSingle();
            Container
                .BindInterfacesAndSelfTo<UnityAdsService>()
                .AsSingle();
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
                .BindInterfacesAndSelfTo<FirebaseRemoteConfigProvider>()
                .AsSingle();
            Container
                .BindInterfacesAndSelfTo<FirebaseAnalyticService>()
                .AsSingle();
        }
        
        
    }
}
