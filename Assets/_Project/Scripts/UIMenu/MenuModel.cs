using Asteroids.SceneManage;
using Asteroids.Total;
using R3;
using Zenject;

namespace Asteroids.Menu
{
	public class MenuModel
	{
		public ReactiveProperty<SaveData> SaveData;
		
		private SceneContainer _sceneContainer;
		
		[Inject]
		private void Construct(SaveManager saveManager, SceneContainer sceneContainer)
		{
			SaveData = saveManager.Data;
			_sceneContainer = sceneContainer;
		}

		public void SetLastScore(int value)
		{ 
			SaveData.Value.LastScore = value;
		}
		
		public void StartGame() => _sceneContainer.LoadGameScene();

		public void ExitGame() => _sceneContainer.CloseGame();
	}
}