using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Asteroids.Input;
using UnityEngine.Events;
using Asteroids.Ship;
using Zenject;

namespace Asteroids.SceneManage
{
    public class GameSceneContainerHandler : IInitializable, ILateDisposable
    {
        private SceneContainer _sceneContainer;
        private InputStorage _inputStorage;

        public void Initialize()
        {
            _inputStorage.EscapeEvent += GoToMenu;
        }

        public void LateDispose()
        {
            _inputStorage.EscapeEvent -= GoToMenu;
        }

        [Inject]
        private void Construct(InputStorage inputStorage, SceneContainer sceneContainer)
        {
            _inputStorage = inputStorage;
            _sceneContainer = sceneContainer;
        }

        public void GoToMenu()
        {
            _sceneContainer.LoadMenuScene();
        }
    }
}

