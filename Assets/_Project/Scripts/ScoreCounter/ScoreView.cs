using Asteroids.Helpers;
using Asteroids.SceneManage;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Asteroids.Score
{
    public class ScoreView : MonoBehaviour, IInitialize
    {
        [SerializeField] private TMP_Text _currenScoreText;
        [SerializeField] private TMP_Text _scoreTextPrefab;
        [SerializeField] private float _lifeTime;

        private Camera _camera;
        private ScoreTextQueue _scoreTextQueue;
        private WaitForSeconds _waitForSeconds;

        public void Initialize(DependencyContainer dependencyContainer = null)
        {
            _camera = Camera.main;
            _scoreTextQueue = new(_scoreTextPrefab, transform);
            _waitForSeconds = new WaitForSeconds(_lifeTime);
        }

        public void ShowCurrentScore(int value) => _currenScoreText.text = "Score: " + value.ToString();

        public void ScoreAdding(int value, Vector2 position) => StartCoroutine(Timer(value, position));

        private IEnumerator Timer(int value, Vector2 position)
        {
            TMP_Text scoreText = _scoreTextQueue.DrawObject();
            scoreText.text = "+" + value.ToString();
            scoreText.transform.position = _camera.WorldToScreenPoint(position);

            yield return _waitForSeconds;

            _scoreTextQueue.ReturnObject(scoreText);
        }

        
    }
}