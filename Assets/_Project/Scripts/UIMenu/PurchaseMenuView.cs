using R3;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using UnityEngine.UI;

namespace Asteroids.Menu
{
    public class PurchaseMenuView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _panel;
        [SerializeField] private Button _toMainMenu;

		private MenuViewModel _viewModel;
		
		[Inject]
		private void Construct(MenuViewModel menuViewModel)
		{
			_viewModel = menuViewModel;
		}
		
        private void Start()
        {
            _viewModel.OpenedWindow
                .Subscribe(w => Show(w == MenuWindow.Purchase))
                .AddTo(this);
            _toMainMenu
                .OnClickAsObservable()
                .Subscribe(_ => ToMainMenu())
                .AddTo(this);
        }

        private void Show(bool value)
        {
            _panel.alpha = value ? 1 : 0;
            _panel.interactable = value;
            _panel.blocksRaycasts = value;
        }

        private void ToMainMenu() => _viewModel.OpenedWindow.Value = MenuWindow.Main;
        
    }
}
