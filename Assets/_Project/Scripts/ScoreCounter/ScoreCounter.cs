using Asteroids.Objects;
using Asteroids.Total;
using Cysharp.Threading.Tasks;
using R3;
using System;
using UnityEngine;
using Zenject;

namespace Asteroids.Score
{
	public class ScoreCounter : IDisposable
	{
		private CompositeDisposable _disposable = new();
		private ScoreViewModel _scoreViewModel;
		private Config _config;

		[Inject]
		private void Construct(
		ScoreViewModel scoreViewModel,
		SaveService saveService,
		[Inject(Id = SpaceObjectID.Asteroid)] SpaceObjectPool asteroid,
		[Inject(Id = SpaceObjectID.Alien)] SpaceObjectPool alien,
		[Inject(Id = SpaceObjectID.SmallAsteroid)] SpaceObjectPool smallAsteroid)
		{
			_config = saveService.DataState.Config;
			_scoreViewModel = scoreViewModel;
			
			alien.ObjectReturned
				.Subscribe(AlienDestroy)
				.AddTo(_disposable);
			asteroid.ObjectReturned
				.Subscribe(AsteroidDestroy)
				.AddTo(_disposable);
			smallAsteroid.ObjectReturned
				.Subscribe(SmallAsteroidDestroy)
				.AddTo(_disposable);
		}

        public void Dispose()
        {
            _disposable.Dispose();
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