using Asteroids.Objects;
using Asteroids.Ship;
using Asteroids.Total;
using R3;
using UnityEngine;
using Zenject;

namespace Asteroids.Analytic
{
    public class AnalyticService : MonoBehaviour
    {
        private IAnalytic _analytic;

        private int _destroyedAsteroids;
        private int _destroyedAliens;
        private int _lasers;
        private int _shots;
        
        [Inject]
        private void Construct(IAnalytic analytic, ObjectManager objectManager)
        { 
            _analytic = analytic;
            
            objectManager.AsteroidQueue.ObjectReturned.Subscribe( _ => AddDestroyedAsteroid()).AddTo(this);
            objectManager.SmallAsteroidQueue.ObjectReturned.Subscribe( _ => AddDestroyedAsteroid()).AddTo(this);
            objectManager.AlienQueue.ObjectReturned.Subscribe(_ => AddDestroyedAliens()).AddTo(this);
            
            objectManager.BulletQueue.ObjectDrawn.Subscribe( _ => AddShots()).AddTo(this);
            objectManager.LaserQueue.ObjectDrawn.Subscribe( _ => AddLaser()).AddTo(this);
        }
    
        private void Start() => _analytic.SendGameStart();
        private void OnDestroy() => _analytic.SendGameStop(_shots, _lasers, _destroyedAsteroids, _destroyedAliens);
        private void AddLaser() => _lasers++;
        private void AddDestroyedAsteroid() => _destroyedAsteroids++;
        private void AddDestroyedAliens() => _destroyedAliens++;
        private void AddShots() =>  _shots++;
    }

}
