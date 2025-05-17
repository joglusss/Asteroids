using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Asteroids.Helpers
{
    public class ScoreTextQueue
    {
        private readonly TMP_Text _objectPrefab;
        private readonly Transform _scoreTextContainer;
        private readonly Queue<TMP_Text> _queue;

        public ScoreTextQueue(TMP_Text objectPrefab, Transform scoreTextContainer)
        {
            _objectPrefab = objectPrefab;
            _scoreTextContainer = scoreTextContainer;
            _queue = new Queue<TMP_Text>();
        }

        private void AddNewObject()
        {
            TMP_Text newScoreText = Object.Instantiate(_objectPrefab, _scoreTextContainer);
            newScoreText.gameObject.SetActive(false);
            _queue.Enqueue(newScoreText);
        }

        public TMP_Text DrawObject()
        {
            if (_queue.Count == 0)
                AddNewObject();
            TMP_Text returnScoreText = _queue.Dequeue();
            returnScoreText.gameObject.SetActive(true);
            return returnScoreText;
        }

        public TMP_Text ReturnObject(TMP_Text returnScoreText)
        {
            if (_queue.Count == 0)
                AddNewObject();
            _queue.Enqueue(returnScoreText);
            returnScoreText.gameObject.SetActive(false);
            return returnScoreText;
        }
    }
}
