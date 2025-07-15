using Asteroids.Helpers;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace Asteroids.Score
{
    public class ScoreCounterView : MonoBehaviour, IInitializable, IDisposable
    {
        [SerializeField] private TMP_Text _scoreTextPrefab;
        [SerializeField] private float _lifeTime;

        private Camera _camera;
        private ScoreTextQueue _scoreTextQueue;
        private WaitForSeconds _waitForSeconds;
        private ScoreCounter _scoreCounter;
        private Label _currentScore;

        [Inject]
        private void Construct(ScoreCounter scoreCounter)
        {
            _scoreCounter = scoreCounter;
        }

        public void Initialize()
        {
            _camera = Camera.main;
            _scoreTextQueue = new(_scoreTextPrefab, transform);
            _waitForSeconds = new WaitForSeconds(_lifeTime);

            _scoreCounter.ObjectDestroyed += ScoreAdding;
        }

        public void Dispose()
        {
            _scoreCounter.ObjectDestroyed -= ScoreAdding;
        }

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