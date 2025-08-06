using System;
using System.Collections;
using Asteroids.Helpers;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace Asteroids.Score
{ 
	public class ScoreView : MonoBehaviour, IInitializable
	{
		// [SerializeField] UIDocument _uiDocument;
		[SerializeField] private TMP_Text _scoreTextPrefab;
		[SerializeField] private float _lifeTime;
		[SerializeField] private TMP_Text _currentScore;

		private ScoreViewModel _scoreViewModel;
		private CompositeDisposable _compositeDisposable = new CompositeDisposable();
		private Camera _camera;
		private ScoreTextQueue _scoreTextQueue;
		private WaitForSeconds _waitForSeconds;
		
		[Inject]
		private void Construct(ScoreViewModel scoreViewModel)
		{
			_scoreViewModel = scoreViewModel;
		}

		public void Initialize()
		{
			_camera = Camera.main;
			_scoreTextQueue = new(_scoreTextPrefab, transform);
			_waitForSeconds = new WaitForSeconds(_lifeTime);
			
			// _currentScore = _uiDocument.rootVisualElement.Q<Label>("LAST_SCORE");

			_scoreViewModel.LastScore.Subscribe(UpdateLastScore).AddTo(_compositeDisposable);
			_scoreViewModel.AddingScore
				.Subscribe(data => StartCoroutine(Timer(data.score, data.position)))
				.AddTo(_compositeDisposable);
		}

		public void Dispose()
		{
			_compositeDisposable.Dispose();
		}
		
		private void UpdateLastScore (int value) => _currentScore.text = $"Score: {value}";
		
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