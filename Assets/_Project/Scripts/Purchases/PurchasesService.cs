using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Cysharp.Threading.Tasks;
using R3;
using Zenject;

namespace Asteroids.Total
{
    public class PurchasesService : IInitializable, IDisposable
    {
        public Subject<bool> PurchaseSucceeded { get; private set; } = new();
    
        private IPurchases _purchases;
        private SaveService _saveService;
        private CompositeDisposable _disposable = new();
        private readonly Dictionary<ProductID, string> _enumToString = new()
        {
            {ProductID.NoAds, "AdsDisable"}
        };

        [Inject]
        private void Construct(IPurchases purchases, SaveService saveService)
        {
            _purchases = purchases;
            _saveService = saveService;
        }

        public void Initialize()
        {
            _purchases.Initialize(_enumToString.Values.ToList());

            _purchases.OnPurchaseSucceeded
                .Subscribe(OnPurchaseSucceeded)
                .AddTo(_disposable);
            _purchases.OnPurchaseFailed
                .Skip(1)
                .Subscribe(OnPurchaseFailed)
                .AddTo(_disposable);
        }
        
        public void Dispose()
        {
            _disposable.Dispose();
        }
        
        public bool CheckProduct(ProductID productID)
        {
            string id = _enumToString[productID];

            return _saveService.Data.PurchasedProduct.Contains(id);
        }

        public void BuyProduct(ProductID productID) => _purchases.BuyProduct(_enumToString[productID]);

        private void OnPurchaseSucceeded((bool,string) ctx)
        {
            PurchaseSucceeded.OnNext(true);
            _saveService.Data.PurchasedProduct.Add(ctx.Item2);
            _saveService.ForceSave();
        }

        private void OnPurchaseFailed(string reason)
        {
            PurchaseSucceeded.OnNext(false);
        }
    }
}
