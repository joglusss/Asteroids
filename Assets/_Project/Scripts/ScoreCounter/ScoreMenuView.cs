using UnityEngine;
using TMPro;
using Asteroids.SceneManage;

namespace Asteroids.Score
{
    public class ScoreMenuView : MonoBehaviour, IInitialize
    {
        [SerializeField] private ScoreManager _scoreManager;
        [SerializeField] private TMP_Text _scoreText;

        public void Initialize(DependencyContainer dependencyContainer = null)
        {
            _scoreText.text = "BEST SCORE: " + _scoreManager.BestScore.ToString() + "\n last score: " + _scoreManager.LastScore.ToString();
        }
    }
}
