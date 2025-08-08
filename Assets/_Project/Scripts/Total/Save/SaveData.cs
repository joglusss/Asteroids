using Newtonsoft.Json;
using R3;
using UnityEngine;

namespace Asteroids.Total
{
	public class SaveData
	{
		[JsonIgnore]
		public ReadOnlyReactiveProperty<int> BestScoreSubscribe => _bestScore;
		[JsonIgnore]
		public ReadOnlyReactiveProperty<int> LastScoreSubscribe => _lastScore;
		
		[JsonProperty]
		private ReactiveProperty<int> _bestScore = new();
		[JsonProperty]
		private ReactiveProperty<int> _lastScore = new();

		[JsonIgnore]
		public int BestScore
		{
			get => _bestScore.Value;
			private set => _bestScore.Value = value;
		}
		[JsonIgnore]
		public int LastScore
		{
			get => _lastScore.Value;
			set
			{
				_bestScore.Value = Mathf.Max(value, BestScore);
				_lastScore.Value = value;
			}
		}
	}
}
