using Asteroids.Input;
using Asteroids.SceneManage;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MenuSceneContainerHandler : IInitializable, ILateDisposable
{
    private SceneContainer _sceneContainer;
    private Button _startGame;
    private Button _exit;

    public void Initialize()
    {
        _startGame.onClick.AddListener(GoToGame);
        _exit.onClick.AddListener(Exit);
    }

    public void LateDispose()
    {
        _startGame.onClick.RemoveListener(GoToGame);
        _exit.onClick.RemoveListener(Exit);
    }

    [Inject]
    private void Construct([Inject(Id = "Start")] Button startGame, [Inject(Id = "Exit")] Button exit, SceneContainer sceneContainer)
    {
        _sceneContainer = sceneContainer;
        _startGame = startGame;
        _exit = exit;
    }

    private void GoToGame()
    {
        _sceneContainer.LoadGameScene();
    }

    private void Exit()
    {
        Application.Quit();
    }

}