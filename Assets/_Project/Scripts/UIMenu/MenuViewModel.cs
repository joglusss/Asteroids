using Asteroids.SceneManage;
using R3;
using Zenject;

namespace Asteroids.Menu
{
	public class MenuViewModel : IInitializable
	{
		public ReadOnlyReactiveProperty<int> BestScore { get; private set; }
		public ReadOnlyReactiveProperty<int> LastScore { get; private set; }
		
		private MenuModel _menuModel;
		
		[Inject]
		private void Construct(MenuModel menuModel)
		{
			_menuModel = menuModel;
		}
		
		public void Initialize()
		{
			BestScore = _menuModel.SaveData.Select(x => x.BestScore).ToReadOnlyReactiveProperty();
			LastScore = _menuModel.SaveData.Select(x => x.LastScore).ToReadOnlyReactiveProperty();
		}		

		public void StartGame() => _menuModel.StartGame();

		public void ExitGame() => _menuModel.ExitGame();
	}
}