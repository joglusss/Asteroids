using R3;
using UnityEngine;

namespace Asteroids.Total
{
	public class SaveData
	{
		private int _bestScore;
		private int _lastScore;

		public int BestScore
		{
			get => _bestScore;
			private set => _bestScore = value;
		}
		public int LastScore
		{
			get => _lastScore;
			set
			{
				_bestScore = Mathf.Max(value, BestScore);
				_lastScore = value;
			}
		}
	}
}
