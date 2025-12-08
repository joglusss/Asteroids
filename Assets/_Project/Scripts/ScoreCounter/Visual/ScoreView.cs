using System.Collections;
using Asteroids.Helpers;
using R3;
using TMPro;
using UnityEngine;
using Zenject;

namespace Asteroids.Score
{ 
	public class ScoreView : MonoBehaviour
	{
		[SerializeField] private TMP_Text _scoreTextPrefab;
		[SerializeField] private float _lifeTime;
		[SerializeField] private TMP_Text _currentScore;

		private ScoreViewModel _scoreViewModel;
		private Camera _camera;
		private ScoreTextQueue _scoreTextQueue;
		private WaitForSeconds _waitForSeconds;
		
		[Inject]
		private void Construct(ScoreViewModel scoreViewModel)
		{
			_scoreViewModel = scoreViewModel;
		}

		private void Start()
		{
			_camera = Camera.main;
			_scoreTextQueue = new(_scoreTextPrefab, transform);
			_waitForSeconds = new WaitForSeconds(_lifeTime);

			_scoreViewModel.LastScore.Subscribe(UpdateLastScore).AddTo(this);
			_scoreViewModel.AddingScore
				.Subscribe(data => StartCoroutine(Timer(data.score, data.position)))
				.AddTo(this);
		}
		
		private void UpdateLastScore (string value) => _currentScore.text = value;
		
		private IEnumerator Timer(string value, Vector2 position)
		{
			TMP_Text scoreText = _scoreTextQueue.DrawObject();
			scoreText.text = value;
			scoreText.transform.position = _camera.WorldToScreenPoint(position);

			yield return _waitForSeconds;

			_scoreTextQueue.ReturnObject(scoreText);
		}
	}
}