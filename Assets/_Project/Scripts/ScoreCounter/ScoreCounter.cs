using UnityEngine;
using Asteroids.Objects;
using Zenject;
using R3;
using Asteroids.Total;

namespace Asteroids.Score
{
	public class ScoreCounter: MonoBehaviour
	{
		private ScoreViewModel _scoreViewModel;
		private Config _config;

		[Inject]
		private void Construct(
		ScoreViewModel scoreViewModel,
		Config config,
		[Inject(Id = SpaceObjectID.Asteroid)] SpaceObjectQueue asteroid,
		[Inject(Id = SpaceObjectID.Alien)] SpaceObjectQueue alien,
		[Inject(Id = SpaceObjectID.SmallAsteroid)] SpaceObjectQueue smallAsteroid)
		{
			_config = config;
			_scoreViewModel = scoreViewModel;
			
			alien.ObjectReturned
				.Subscribe(AlienDestroy)
				.AddTo(this);
			asteroid.ObjectReturned
				.Subscribe(AsteroidDestroy)
				.AddTo(this);
			smallAsteroid.ObjectReturned
				.Subscribe(SmallAsteroidDestroy)
				.AddTo(this);
		}

		private void CallMethods(int value, SpaceObject a)
		{
			_scoreViewModel.AddScore(value, (Vector2)a.transform.position);
		}

		private void AsteroidDestroy(SpaceObject a) => CallMethods(_config.AsteroidCost, a);

		private void SmallAsteroidDestroy(SpaceObject a) => CallMethods(_config.SmallAsteroidCost,a);

		private void AlienDestroy(SpaceObject a) => CallMethods(_config.AlienCost, a);
	}
}