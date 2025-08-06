
using System;
using Asteroids.Total;
using R3;
using UnityEngine;
using Zenject;

namespace Asteroids.Score
{
	public class ScoreViewModel : IInitializable
	{
		public ReadOnlyReactiveProperty<int> LastScore { get; private set; }
		public ReactiveCommand<(int score, Vector2 position)> AddingScore { get; private set; } = new();
		
		private ScoreModel _scoreModel;
		
		[Inject]
		private void Construct(ScoreModel scoreModel)
		{ 
			_scoreModel = scoreModel;
		}

		public void Initialize()
		{
			LastScore = Observable.EveryValueChanged(_scoreModel.SaveData, x => x.Value.LastScore).ToReadOnlyReactiveProperty();
		}

		public void AddScore(int value, Vector2 position)
		{
			_scoreModel.AddScore(value);
			AddingScore.Execute((value, position));
		}
	}
}

