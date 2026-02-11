using Asteroids.SceneManage;
using Asteroids.Total;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Asteroids.Menu
{
	public class MenuModel
	{
		public SaveDataState SaveData;
		
		private SceneContainer _sceneContainer;
		private SaveService _saveService;

        [Inject]
		private void Construct(SaveService saveService, SceneContainer sceneContainer)
		{
			SaveData = saveService.DataState;

			_saveService = saveService;
            _sceneContainer = sceneContainer;
		}
		
		public void StartGame() => _sceneContainer.LoadGameScene();

		public async UniTask ExitGame()
		{
			await _saveService.ForceSave();
            _sceneContainer.CloseGame();
        }
	}
}