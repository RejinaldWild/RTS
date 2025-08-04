using _RTSGameProject.Logic.Ads;
using _RTSGameProject.Logic.Shop.Model;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;
using UnityEngine.Purchasing.Security;
using Zenject;

namespace _RTSGameProject.Logic.Common.Services
{
    public class PurchaseService: IPurchase, IDetailedStoreListener, IInitializable
    {
        private IStoreController _storeController;
        private NonConsumableItem _nonConsumableItem;
        private IAdsService _adsService;

        public PurchaseService(IAdsService adsService, NonConsumableItem nonConsumableItem)
        {
            _adsService = adsService;
            _nonConsumableItem = nonConsumableItem;
        }
        
        public void Initialize()
        {
            _nonConsumableItem.Id = "no_ads";
            _nonConsumableItem.Name = "NoAds";
            _nonConsumableItem.Price = 2;
            _nonConsumableItem.Description = "No Ads in the future game";
            
            ConfigurationBuilder builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
            builder.AddProduct(_nonConsumableItem.Id, ProductType.NonConsumable);
            
            UnityPurchasing.Initialize(this, builder);
        }
        
        public void Payment()
        {
            _storeController.InitiatePurchase(_nonConsumableItem.Id);
        }

        private void RemoveAds()
        {
            _adsService.RemoveAds();
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            Debug.Log("Purchase failed: " + error);
        }

        public void OnInitializeFailed(InitializationFailureReason error, string message)
        {
            Debug.Log("Purchase failed: " + error + "| message of error: " + message);
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
        {
            var product = purchaseEvent.purchasedProduct.definition.id;
            if (product == _nonConsumableItem.Id)
            {
                RemoveAds();
            }
            
            return PurchaseProcessingResult.Complete;
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            Debug.Log("Purchase failed " + failureReason);
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            Debug.Log("Initialized Purchase Service");
            _storeController = controller;
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
        {
            Debug.Log("Purchase failed " + failureDescription);
        }
    }
}