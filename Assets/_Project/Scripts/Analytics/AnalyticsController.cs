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
        private StopGameLogParameters _parameters;
        
        [Inject]
        private void Construct(
            IAnalyticsService analytic,
            [Inject(Id = SpaceObjectID.Asteroid)] SpaceObjectQueue asteroid,
            [Inject(Id = SpaceObjectID.Alien)] SpaceObjectQueue alien,
            [Inject(Id = SpaceObjectID.SmallAsteroid)] SpaceObjectQueue smallAsteroid,
            [Inject(Id = SpaceObjectID.Bullet)] SpaceObjectQueue bullet,
            [Inject(Id = SpaceObjectID.Laser)] SpaceObjectQueue laser
        )
        { 
            _analytic = analytic;
            
            asteroid.ObjectReturned.Subscribe( _ => AddDestroyedAsteroid()).AddTo(_disposable);
            smallAsteroid.ObjectReturned.Subscribe( _ => AddDestroyedAsteroid()).AddTo(_disposable);
            alien.ObjectReturned.Subscribe(_ => AddDestroyedAliens()).AddTo(_disposable);
            
            bullet.ObjectDrawn.Subscribe( _ => AddShots()).AddTo(_disposable);
            laser.ObjectDrawn.Subscribe( _ => AddLaser()).AddTo(_disposable);
        }
    
        public void Initialize() => _analytic.SendGameStart();
        
        public void Dispose() 
        {
           _analytic.SendGameStop(_parameters);
           _disposable.Dispose();
        } 
        
        private void AddLaser() 
        {
            _parameters.LasersCount++;
            _analytic.SendLaserUsed();
        }
        
        private void AddDestroyedAsteroid() => _parameters.DestroyedAsteroidsCount++;
        private void AddDestroyedAliens() => _parameters.DestroyedAliensCount++;
        private void AddShots() =>  _parameters.ShootsCount++;
    }

}
