
using System;
using Asteroids.Total;
using R3;
using UnityEngine;
using Zenject;

namespace Asteroids.Score
{
	public class ScoreViewModel : IInitializable
	{
		public ReadOnlyReactiveProperty<string> LastScore { get; private set; }
		public ReactiveCommand<(string score, Vector2 position)> AddingScore { get; private set; } = new();
		
		private ScoreModel _scoreModel;
		
		[Inject]
		private void Construct(ScoreModel scoreModel)
		{ 
			_scoreModel = scoreModel;
		}

		public void Initialize()
		{
			LastScore = _scoreModel.SaveData.LastScoreSubscribe.Select(x => $"Score: {x}").ToReadOnlyReactiveProperty();
		}

		public void AddScore(int value, Vector2 position)
		{
			_scoreModel.AddScore(value);
			AddingScore.Execute(($"+{value}", position));
		}
	}
}

