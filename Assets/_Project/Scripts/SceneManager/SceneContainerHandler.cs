using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Asteroids.Input;
using UnityEngine.Events;

namespace Asteroids.SceneManage
{
    public class SceneContainerHandler : MonoBehaviour
    {
        [SerializeField] private SceneContainer _sceneContainer;

        public void GoToMenu()
        {
            _sceneContainer.LoadMenuScene();
        }

        public void GoToGame()
        {
            _sceneContainer.LoadGameScene();
        }

        public void Exit()
        {
            Application.Quit();
        }
    }
}

