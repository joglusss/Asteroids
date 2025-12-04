using Asteroids.Total;
using UnityEngine;
using Zenject;

namespace Asteroids.Score
{
	public class ScoreModel : IInitializable
	{
		public SaveDataState SaveData { get; private set; }
		
		[Inject]
		private void Construct(SaveService saveManager)
		{
			SaveData = saveManager.DataState;
		}
				
		public void Initialize()
		{
			SaveData.LastScore.Value = 0;
		}
		
		public void AddScore(int value) 
		{
			SaveData.LastScore.Value += value;
		} 
	}
}

