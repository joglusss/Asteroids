using Asteroids.SceneManage;
using Asteroids.Total;
using R3;
using UnityEngine;
using Zenject;

namespace Asteroids.Menu
{
	public class MenuModel
	{
		public SaveData SaveData;
		
		private SceneContainer _sceneContainer;
		
		[Inject]
		private void Construct(SaveService saveManager, SceneContainer sceneContainer)
		{
			SaveData = saveManager.Data;
			_sceneContainer = sceneContainer;
		}
		
		public void StartGame() => _sceneContainer.LoadGameScene();

		public void ExitGame() => _sceneContainer.CloseGame();
	}
}