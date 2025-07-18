using _RTSGameProject.Logic.Ads.UnityAds;
using _RTSGameProject.Logic.Analytic.Firebase;
using _RTSGameProject.Logic.Common.SaveLoad;
using _RTSGameProject.Logic.Common.Services;
using _RTSGameProject.Logic.Shop.Model;
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
            BindInAppPurchase();
        }

        private void BindInAppPurchase()
        {
            Container
                .Bind<NonConsumableItem>()
                .AsSingle();
            Container
                .BindInterfacesAndSelfTo<PurchaseService>()
                .AsSingle();
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
                .BindInterfacesAndSelfTo<PlayerPrefsDataStorage>()
                .AsSingle();
            Container
                .BindInterfacesAndSelfTo<JsonConverter>()
                .AsSingle();
            Container
                .BindInterfacesAndSelfTo<SaveService>()
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
