using Asteroids.Total;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Asteroids.Menu
{
    public class PurchaseItemView : MonoBehaviour
    {
        [SerializeField] private ProductID _id;
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _text;
        
        [Inject] private MenuViewModel _menuViewModel;
        
        public void Start()
        {
            CheckBoughtStatus();
            _button.OnClickAsObservable().Subscribe(_ => SendRequest()).AddTo(this);

            _menuViewModel.OnPurchasedProductChange
                .Subscribe(_ => CheckBoughtStatus())
                .AddTo(this);
        }
        
        private void SendRequest()
        {
            _menuViewModel.BuyingRequest(_id);
        }

        private void CheckBoughtStatus()
        {
            if (!_menuViewModel.ItemIsBought(_id))
            {
                _text.text = "Buy";
                _button.interactable = true;
            }
            else
            {
                _text.text = "Purchased";
                _button.interactable = false;
            }
        }
    }
}

