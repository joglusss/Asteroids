using UnityEngine;
using TMPro;
using Zenject;
using Asteroids.Total;
using R3;
using UnityEngine.UIElements;

namespace Asteroids.Score
{
    public class LastScoreView : MonoBehaviour, IInitializable, ILateDisposable
    {
        [SerializeField] UIDocument _uiDocument;
        [SerializeField] private string _startText;

        private DataHandler _dataHandler;
        private CompositeDisposable _compositeDisposable = new CompositeDisposable();
        private Label _currentScore;

        public void Initialize()
        {
            _currentScore = _uiDocument.rootVisualElement.Q<Label>("LAST_SCORE");
            _compositeDisposable.Add(_dataHandler.LastScoreSub.Subscribe(UpdateText));
            UpdateText(0);
        }

        public void LateDispose()
        {
            _compositeDisposable.Dispose();
        }

        private void UpdateText(int value)
        {
            _currentScore.text = _startText + _dataHandler.LastScore;
        }

        [Inject]
        private void Construct(DataHandler dataSaver)
        {
            _dataHandler = dataSaver;
        }
    }
}