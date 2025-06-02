using UnityEngine;
using TMPro;
using Zenject;

namespace Asteroids.Score
{
    public class LastScoreView : MonoBehaviour, IInitializable, ILateDisposable
    {
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private string _startText;

        private ScoreManager _scoreManager;

        public void Initialize()
        {
            _scoreManager.LastScoreChanged += UpdateText;
            UpdateText(0);
        }

        public void LateDispose()
        {
            _scoreManager.LastScoreChanged -= UpdateText;
        }

        private void UpdateText(int value)
        {
            _scoreText.text = _startText + _scoreManager.LastScore;
        }

        [Inject]
        private void Construct(ScoreManager scoreManager)
        {
            _scoreManager = scoreManager;
        }
    }
}