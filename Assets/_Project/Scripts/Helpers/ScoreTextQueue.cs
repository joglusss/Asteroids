using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Asteroids.Helpers
{
    public class ScoreTextQueue
    {
        private GameObject _objectPrefab;
        private Transform _scoreTextContainer;
        private Queue<TMP_Text> _queue;

        public ScoreTextQueue(GameObject objectPrefab, Transform scoreTextContainer)
        {
            _objectPrefab = objectPrefab;
            _scoreTextContainer = scoreTextContainer;
            _queue = new Queue<TMP_Text>();
        }

        private void AddNewObject()
        {
            if (!MonoBehaviour.Instantiate(_objectPrefab, _scoreTextContainer).TryGetComponent(out TMP_Text newScoreText))
            {
                Debug.LogError("ObjectPrefab doesn't have a component");
                return;
            }

            Queue<TMP_Text> link = _queue;

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
