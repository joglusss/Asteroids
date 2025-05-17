using UnityEngine;
using Asteroids.Objects;
using Asteroids.SceneManage;

namespace Asteroids.Score
{
    public class ScoreCounter: MonoBehaviour, IInitialize
    {
        [SerializeField] private int AsteroidCost;
        [SerializeField] private int SmallAsteroidCost;
        [SerializeField] private int AlienCost;

        private ObjectManager _objectManagerLink;
        private ScoreManager _scoreManagerLink;
        private ScoreView _scoreView;

        private void OnDestroy() 
        {
            _objectManagerLink.AlienQueue.ObjectReturnedToQueue -= AlienDestroy;
            _objectManagerLink.AsteroidQueue.ObjectReturnedToQueue -= AsteroidDestroy;
            _objectManagerLink.SmallAsteroidQueue.ObjectReturnedToQueue -= SmallAsteroidDestroy;
        }

        public void Initialize(DependencyContainer dependencyContainer)
        {
            _objectManagerLink = dependencyContainer.ObjectManagerLink;
            _scoreManagerLink = dependencyContainer.ScoreManagerLink;
            _scoreView = dependencyContainer.ScoreViewLink;

            _objectManagerLink.AlienQueue.ObjectReturnedToQueue += AlienDestroy;
            _objectManagerLink.AsteroidQueue.ObjectReturnedToQueue += AsteroidDestroy;
            _objectManagerLink.SmallAsteroidQueue.ObjectReturnedToQueue += SmallAsteroidDestroy;

            _scoreManagerLink.ResetLastScore();
            _scoreView.ShowCurrentScore(_scoreManagerLink.LastScore);
        }

        private void CallMethods(int value, SpaceObject a)
        {
            _scoreManagerLink.AddScore(value);
            _scoreView.ShowCurrentScore(_scoreManagerLink.LastScore);
            _scoreView.ScoreAdding(value, (Vector2)a.transform.position);
        }

        private void AsteroidDestroy(SpaceObject a) => CallMethods(AsteroidCost, a);

        private void SmallAsteroidDestroy(SpaceObject a) => CallMethods(SmallAsteroidCost,a);

        private void AlienDestroy(SpaceObject a) => CallMethods(AlienCost, a);
    }
}