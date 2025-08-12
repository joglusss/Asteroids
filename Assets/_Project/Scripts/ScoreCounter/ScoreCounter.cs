using UnityEngine;
using Asteroids.Objects;
using Zenject;
using R3;

namespace Asteroids.Score
{
	public class ScoreCounter: MonoBehaviour, IInitializable
	{
		[SerializeField] private int AsteroidCost;
		[SerializeField] private int SmallAsteroidCost;
		[SerializeField] private int AlienCost;

		private ObjectManager _objectManagerLink;
		private ScoreViewModel _scoreViewModel;

		[Inject]
		private void Construct(ObjectManager objectManager, ScoreViewModel scoreViewModel)
		{
			_objectManagerLink = objectManager;
			_scoreViewModel = scoreViewModel;
		}
		public void Initialize()
		{
			_objectManagerLink.AlienQueue
				.ObjectReturned
				.Subscribe(AlienDestroy)
				.AddTo(this);
			_objectManagerLink.AsteroidQueue
				.ObjectReturned
				.Subscribe(AsteroidDestroy)
				.AddTo(this);
			_objectManagerLink.SmallAsteroidQueue
				.ObjectReturned
				.Subscribe(SmallAsteroidDestroy)
				.AddTo(this);
		}

		private void CallMethods(int value, SpaceObject a)
		{
			_scoreViewModel.AddScore(value, (Vector2)a.transform.position);
		}

		private void AsteroidDestroy(SpaceObject a) => CallMethods(AsteroidCost, a);

		private void SmallAsteroidDestroy(SpaceObject a) => CallMethods(SmallAsteroidCost,a);

		private void AlienDestroy(SpaceObject a) => CallMethods(AlienCost, a);
	}
}