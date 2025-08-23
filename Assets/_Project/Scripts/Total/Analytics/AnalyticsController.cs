using System;
using Asteroids.Objects;
using Asteroids.Total;
using R3;
using Zenject;

namespace Asteroids.Analytic
{
    public class AnalyticsController : IInitializable, IDisposable
    {
        private IAnalyticsService _analytic;
        private CompositeDisposable _disposable = new();
        private int _destroyedAsteroids;
        private int _destroyedAliens;
        private int _lasers;
        private int _shots;
        
        [Inject]
        private void Construct(IAnalyticsService analytic, ObjectManager objectManager)
        { 
            _analytic = analytic;
            
            objectManager.AsteroidQueue.ObjectReturned.Subscribe( _ => AddDestroyedAsteroid()).AddTo(_disposable);
            objectManager.SmallAsteroidQueue.ObjectReturned.Subscribe( _ => AddDestroyedAsteroid()).AddTo(_disposable);
            objectManager.AlienQueue.ObjectReturned.Subscribe(_ => AddDestroyedAliens()).AddTo(_disposable);
            
            objectManager.BulletQueue.ObjectDrawn.Subscribe( _ => AddShots()).AddTo(_disposable);
            objectManager.LaserQueue.ObjectDrawn.Subscribe( _ => AddLaser()).AddTo(_disposable);
        }
    
        public void Initialize() => _analytic.SendGameStart();
        
        public void Dispose() 
        {
           _analytic.SendGameStop(_shots, _lasers, _destroyedAsteroids, _destroyedAliens);
           _disposable.Dispose();
        } 
        
        private void AddLaser() 
        {
            _lasers++;
            _analytic.SendLaserUsed();
        }
        
        private void AddDestroyedAsteroid() => _destroyedAsteroids++;
        private void AddDestroyedAliens() => _destroyedAliens++;
        private void AddShots() =>  _shots++;
    }

}
