using UnityEngine;
using Asteroids.Objects;
using Zenject;
using System;
using Asteroids.Total;

namespace Asteroids.Score
{
	public class ScoreCounter: MonoBehaviour, IInitializable, IDisposable
	{
		[SerializeField] private int AsteroidCost;
		[SerializeField] private int SmallAsteroidCost;
		[SerializeField] private int AlienCost;

		public event Action<int, Vector2> ObjectDestroyed;

		private ObjectManager _objectManagerLink;
		private ScoreViewModel _scoreViewModel;

		[Inject]
		private void Construct(ObjectManager objectManager, ScoreViewModel scoreViewModel)
		{
			_objectManagerLink = objectManager;
			_scoreViewModel = scoreViewModel;
		}

		public void Dispose()
		{
			_objectManagerLink.AlienQueue.ObjectReturnedToQueue -= AlienDestroy;
			_objectManagerLink.AsteroidQueue.ObjectReturnedToQueue -= AsteroidDestroy;
			_objectManagerLink.SmallAsteroidQueue.ObjectReturnedToQueue -= SmallAsteroidDestroy;
		}

		public void Initialize()
		{
			_objectManagerLink.AlienQueue.ObjectReturnedToQueue += AlienDestroy;
			_objectManagerLink.AsteroidQueue.ObjectReturnedToQueue += AsteroidDestroy;
			_objectManagerLink.SmallAsteroidQueue.ObjectReturnedToQueue += SmallAsteroidDestroy;
		}

		private void CallMethods(int value, SpaceObject a)
		{
			ObjectDestroyed?.Invoke(value, (Vector2)a.transform.position);
			_scoreViewModel.AddScore(value, (Vector2)a.transform.position);
		}

		private void AsteroidDestroy(SpaceObject a) => CallMethods(AsteroidCost, a);

		private void SmallAsteroidDestroy(SpaceObject a) => CallMethods(SmallAsteroidCost,a);

		private void AlienDestroy(SpaceObject a) => CallMethods(AlienCost, a);
	}
}