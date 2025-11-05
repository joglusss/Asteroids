using System.Collections.Generic;
using R3;
using UnityEngine;

namespace Asteroids.Total
{
    public interface IPurchases
    {
        void Initialize(List<string> purchaseID);
        void BuyProduct(string id);
        
        Subject<(bool, string)> OnPurchaseSucceeded { get; set; }
        ReactiveProperty<string> OnPurchaseFailed { get; set; }
        ReactiveProperty<bool> OnPurchaseWaiting { get; set; }
    }
}