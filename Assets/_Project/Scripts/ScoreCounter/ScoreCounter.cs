using UnityEngine;
using Asteroids.Objects;
using Zenject;
using System;

namespace Asteroids.Score
{
    public class ScoreCounter: MonoBehaviour, IInitializable, ILateDisposable
    {
        [SerializeField] private int AsteroidCost;
        [SerializeField] private int SmallAsteroidCost;
        [SerializeField] private int AlienCost;

        public event Action<int, Vector2> ObjectDestroyed;

        private ObjectManager _objectManagerLink;
        private ScoreManager _scoreManagerLink;

        public void LateDispose()
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

            _scoreManagerLink.ResetLastScore();
        }

        [Inject]
        private void Construct(ObjectManager objectManager, ScoreManager scoreManager)
        {
            _objectManagerLink = objectManager;
            _scoreManagerLink = scoreManager;
        }

        private void CallMethods(int value, SpaceObject a)
        {
            _scoreManagerLink.AddScore(value);
            ObjectDestroyed?.Invoke(value, (Vector2)a.transform.position);
        }

        private void AsteroidDestroy(SpaceObject a) => CallMethods(AsteroidCost, a);

        private void SmallAsteroidDestroy(SpaceObject a) => CallMethods(SmallAsteroidCost,a);

        private void AlienDestroy(SpaceObject a) => CallMethods(AlienCost, a);
    }
}