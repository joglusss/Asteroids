using R3;
using UnityEngine;
using Zenject;

namespace Asteroids.Menu
{
	public class MenuViewModel : IInitializable
	{
		public ReadOnlyReactiveProperty<string> BestScore { get; private set; }
		public ReadOnlyReactiveProperty<string> LastScore { get; private set; }
		
		private MenuModel _menuModel;
		
		[Inject]
		private void Construct(MenuModel menuModel)
		{
			_menuModel = menuModel;
		}
		
		public void Initialize()
		{
			BestScore = _menuModel.SaveData.BestScoreSubscribe
				.Select(x => $"Best Score:  {x}")
				.ToReadOnlyReactiveProperty();
			LastScore = _menuModel.SaveData.LastScoreSubscribe
				.Select(x => $"Last Score:  {x}")
				.ToReadOnlyReactiveProperty();
		}		

		public void StartGame() => _menuModel.StartGame();

		public void ExitGame() => _menuModel.ExitGame();
	}
}