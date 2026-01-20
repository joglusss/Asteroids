using System;
using System.Collections.Generic;
using System.Linq;
using R3;
using UnityEngine;
using UnityEngine.Purchasing;

namespace Asteroids.Total
{
    public class IAPurchase : IPurchases, IDisposable
    {
        public Subject<(bool, string)> OnPurchaseSucceeded { get; set; } = new();
        public ReactiveProperty<string> OnPurchaseFailed { get; set; } = new();
        public ReactiveProperty<bool> OnPurchaseWaiting { get; set; } = new();

        private StoreController _storeController;

        public void Initialize(List<string> purchaseID)
        {
            _storeController = UnityIAPServices.StoreController();
            
            _storeController.OnPurchasePending += OnPurchasePendingHandler;  

            _storeController.Connect();
            
            _storeController.OnProductsFetched += OnProductsFetchedHandler;
            _storeController.OnPurchasesFetched += OnPurchasesFetched;  

            _storeController.OnPurchaseConfirmed += OnPurchaseConfirmHandler;
            _storeController.OnPurchaseFailed += OnPurchaseFailedHandler;

            List<ProductDefinition> initialProductToFetch = purchaseID.Select(x => new ProductDefinition(x, ProductType.NonConsumable)).ToList();
            
            _storeController.FetchProducts(initialProductToFetch);
        }
        
        public void Dispose()
        {
            _storeController.OnPurchasePending -= OnPurchasePendingHandler;  
            _storeController.OnProductsFetched -= OnProductsFetchedHandler;
            _storeController.OnPurchasesFetched -= OnPurchasesFetched;  
            _storeController.OnPurchaseConfirmed -= OnPurchaseConfirmHandler;
            _storeController.OnPurchaseFailed -= OnPurchaseFailedHandler;
        }
        
        public void BuyProduct(string id)
        {
            _storeController.PurchaseProduct(id);
        }

        private void OnPurchaseConfirmHandler(Order order)
        {
            OnPurchaseSucceeded.OnNext((true,order.Info.PurchasedProductInfo[0].productId));
            OnPurchaseWaiting.Value = false;
        }

        private void OnPurchaseFailedHandler(FailedOrder order)
        {
            OnPurchaseSucceeded.OnNext((false, string.Empty));
            OnPurchaseFailed.OnNext(order.FailureReason.ToString());
            OnPurchaseWaiting.Value = false;
        }

        private void OnPurchasePendingHandler(PendingOrder order)
        {
            _storeController.ConfirmPurchase(order);
            OnPurchaseWaiting.Value = true;
        }
        
        private void OnProductsFetchedHandler(List<Product> products)  
        {
            Debug.Log("OnProductsFetchedHandler");
            _storeController.FetchPurchases();  
        }  
        
        private void OnPurchasesFetched(Orders orders) {
            Debug.Log("OnProductsFetchedHandler");
        }
    }
}
