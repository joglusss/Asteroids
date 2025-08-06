using Asteroids.Total;
using R3;
using Zenject;

namespace Asteroids.Score
{
	public class ScoreModel : IInitializable
	{
		public ReactiveProperty<SaveData> SaveData;
		
		[Inject]
		private void Construct(SaveManager saveManager)
		{ 
			SaveData = saveManager.Data;
		}
				
		public void Initialize()
		{
			SaveData.Value.LastScore = 0;
		}

		public void AddScore(int value) 
		{
			SaveData.Value.LastScore += value;
		} 
	}
}

