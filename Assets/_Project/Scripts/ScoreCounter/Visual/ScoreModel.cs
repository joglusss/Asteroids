using Asteroids.Total;
using R3;
using Unity.Collections;
using Zenject;

namespace Asteroids.Score
{
	public class ScoreModel : IInitializable
	{
		public SaveData SaveData { get; private set; }
		
		[Inject]
		private void Construct(SaveService saveManager)
		{ 
			SaveData = saveManager.Data;
		}
				
		public void Initialize()
		{
			SaveData.LastScore = 0;
		}
		
		public void AddScore(int value) 
		{
			SaveData.LastScore += value;
		} 
	}
}

