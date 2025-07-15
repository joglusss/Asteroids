using UnityEngine;
using TMPro;
using Zenject;
using Asteroids.Total;
using R3;
using UnityEngine.UIElements;
using System;

namespace Asteroids.Score
{
    public class LastScoreView : MonoBehaviour, IInitializable, IDisposable
    {
        [SerializeField] UIDocument _uiDocument;
        [SerializeField] private string _startText;

        private SaveManager _dataHandler;
        private CompositeDisposable _compositeDisposable = new CompositeDisposable();
        private Label _currentScore;

        [Inject]
        private void Construct(SaveManager dataSaver)
        {
            _dataHandler = dataSaver;
        }

        public void Initialize()
        {
            _currentScore = _uiDocument.rootVisualElement.Q<Label>("LAST_SCORE");
            _compositeDisposable.Add(_dataHandler.LastScoreSub.Subscribe(UpdateText));
            UpdateText(0);
        }

        public void Dispose()
        {
            _compositeDisposable.Dispose();
        }

        private void UpdateText(int value)
        {
            _currentScore.text = _startText + _dataHandler.LastScore;
        }
    }
}