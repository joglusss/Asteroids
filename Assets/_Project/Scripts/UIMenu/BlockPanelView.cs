using R3;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Asteroids.Menu
{
    public class BlockPanelView : MonoBehaviour, IInitializable
    {
        [SerializeField] private CanvasGroup _panel;
        
        [Inject] private MenuViewModel _menuViewModel;

        public void Initialize()
        {
            _menuViewModel.TransactionStarted.Subscribe(Show).AddTo(this);
            Show(false);
        }

        private void Show(bool value)
        {
            _panel.alpha = value ? 1 : 0;
            _panel.interactable = value;
            _panel.blocksRaycasts = value;
        }
    }
}


