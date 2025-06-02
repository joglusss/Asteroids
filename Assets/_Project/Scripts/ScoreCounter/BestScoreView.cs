using UnityEngine;
using TMPro;
using Asteroids.SceneManage;
using Zenject;
using static UnityEngine.Rendering.DebugUI;

namespace Asteroids.Score
{
    public class BestScoreView : MonoBehaviour, IInitializable
    {
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private string _startText;

        private ScoreManager _scoreManager;

        public void Initialize() => _scoreText.text = _startText + _scoreManager.BestScore;

        [Inject]
        private void Construct(ScoreManager scoreManager) => _scoreManager = scoreManager;

    }
}