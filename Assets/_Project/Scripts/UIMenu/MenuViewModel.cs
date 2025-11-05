using Asteroids.Total;
using Cysharp.Threading.Tasks;
using ObservableCollections;
using R3;
using UnityEngine;
using Zenject;

namespace Asteroids.Menu
{
	public class MenuViewModel : IInitializable
	{
		public ReadOnlyReactiveProperty<string> BestScore { get; private set; }
		public ReadOnlyReactiveProperty<string> LastScore { get; private set; }
		public ReactiveProperty<MenuWindow> OpenedWindow { get; private set; } = new(MenuWindow.Main);
		public ReadOnlyReactiveProperty<string> OnPurchasedProductChange { get; private set; }
		public Subject<bool> TransactionStarted { get; private set; } = new();
		
		private MenuModel _menuModel;
		private PurchasesService _purchasesService;
		
		[Inject]
		private void Construct(MenuModel menuModel,PurchasesService puchasesService)
		{
			_menuModel = menuModel;
			_purchasesService = puchasesService;
		}
		
		public void Initialize()
		{
			_purchasesService.PurchaseSucceeded.Subscribe(_ => TransactionStarted.OnNext(false));
		
			BestScore = _menuModel.SaveData.BestScoreSubscribe
				.Select(x => $"Best Score:  {x}")
				.ToReadOnlyReactiveProperty();
			LastScore = _menuModel.SaveData.LastScoreSubscribe
				.Select(x => $"Last Score:  {x}")
				.ToReadOnlyReactiveProperty();

			OnPurchasedProductChange = _menuModel.SaveData.PurchasedProduct
				.ObserveChanged()
				.Select(x => x.NewItem)
				.ToReadOnlyReactiveProperty();
		}

		public bool ItemIsBought(ProductID iD) => _purchasesService.CheckProduct(iD);

		public void BuyingRequest(ProductID iD)
        {
			TransactionStarted.OnNext(true);
			_purchasesService.BuyProduct(iD);
        }		

		public void StartGame() => _menuModel.StartGame();

		public void ExitGame() => _menuModel.ExitGame();
	}
}