using Asteroids.Input;
using Asteroids.SceneManage;
using Asteroids.Total;
using R3;
using System;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace Asteroids.SceneManage
{
    public class MenuSceneService : MonoBehaviour, IInitializable, IDisposable
    {
        [SerializeField] private UIDocument _uIDocument;

        private CompositeDisposable _compositeDisposable = new CompositeDisposable();
        private SceneContainer _sceneContainer;
        private SaveManager _dataHandler;
        private Button _startGame;
        private Button _exit;
        private Label _bestScore;
        private Label _lastScore;

        [Inject]
        private void Construct(SceneContainer sceneContainer, SaveManager dataHandler)
        {
            _sceneContainer = sceneContainer;
            _dataHandler = dataHandler;
        }

        public void Initialize()
        {
            _startGame = _uIDocument.rootVisualElement.Q<Button>("START_GAME");
            _exit = _uIDocument.rootVisualElement.Q<Button>("EXIT");
            _bestScore = _uIDocument.rootVisualElement.Q<Label>("BEST_SCORE");
            _lastScore = _uIDocument.rootVisualElement.Q<Label>("LAST_SCORE");

            _startGame.RegisterCallback<ClickEvent>(GoToGame);
            _exit.RegisterCallback<ClickEvent>(Exit);

            _compositeDisposable.Add(_dataHandler.BestScoreSub.Subscribe(UpdateBestScore));
            _compositeDisposable.Add(_dataHandler.LastScoreSub.Subscribe(UpdateLastScore));
        }

        public void Dispose()
        {
            _startGame.UnregisterCallback<ClickEvent>(GoToGame);
            _exit.UnregisterCallback<ClickEvent>(Exit);

            _compositeDisposable.Dispose();
        }

        private void GoToGame(ClickEvent e)
        {
            _sceneContainer.LoadGameScene();
        }

        private void Exit(ClickEvent e)
        {
            Application.Quit();
        }

        private void UpdateBestScore(int value) => _bestScore.text = "BEST SCORE: " + value;

        private void UpdateLastScore(int value) => _lastScore.text = "Last score: " + value;
    }
}
