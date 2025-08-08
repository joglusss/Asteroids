using Asteroids.Input;
using Zenject;
using System;

namespace Asteroids.SceneManage
{
	public class SceneService : IInitializable, IDisposable
	{
		private SceneContainer _sceneContainer;
		private IInput _inputStorage;
		
        [Inject]
        private void Construct(IInput inputStorage, SceneContainer sceneContainer)
        {
            _inputStorage = inputStorage;
            _sceneContainer = sceneContainer;
        }

		public void Initialize()
		{
			_inputStorage.EscapeEvent += GoToMenu;
		}

		public void Dispose()
		{
			_inputStorage.EscapeEvent -= GoToMenu;
		}

		public void GoToMenu()
		{
			_sceneContainer.LoadMenuScene();
		}
	}
}

