using _RTSGameProject.Logic.Ads.UnityAds;
using _RTSGameProject.Logic.Analytic.Firebase;
using _RTSGameProject.Logic.Common.SaveLoad;
using _RTSGameProject.Logic.Common.Services;
using _RTSGameProject.Logic.Common.Services.SoundFX;
using _RTSGameProject.Logic.Common.Services.VFX;
using _RTSGameProject.Logic.Shop.Model;
using UnityEngine;
using Zenject;

namespace _RTSGameProject.Logic.Bootstrap
{
    public class EntryPointInstaller : MonoInstaller
    {
        enum SaveLoad
        {
            LocalSaveLoad,
            CloudSaveLoad
        }
        
        [SerializeField] private AudioService _audioService;
        
        public override void InstallBindings()
        {
            
            BindInternetConnectionChecker();
            BindSaveService();
            BindFirebase();
            BindAds();
            BindInAppPurchase();
            BindAudioService();
        }

        private void BindAudioService()
        {
            Container
                .Bind<IAudio>()
                .To<AudioService>()
                .FromComponentInNewPrefab(_audioService)
                .AsSingle();
        }

        private void BindInternetConnectionChecker()
        {
            Container
                .Bind<InternetConnectionChecker>()
                .AsSingle();
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
                .Bind<ISaveService>()
                .WithId(SaveLoad.LocalSaveLoad.ToString())
                .To<LocalSaveLoadService>()
                .AsSingle();
            Container
                .Bind<ISaveService>()
                .WithId(SaveLoad.CloudSaveLoad.ToString())
                .To<CloudSaveLoadService>()
                .AsSingle();
            Container
                .Bind<ISaveService>().To<SaveService>()
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
