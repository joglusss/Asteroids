using Asteroids.Objects;
using UnityEngine;
using Zenject;

namespace Asteroids.Installers
{
    public class ObjectManagerInstaller : MonoInstaller
    {
        [SerializeField] private ObjectManager _objectManager;
        [SerializeField] private SpaceObject _bulletPrefab;
        [SerializeField] private SpaceObject _asteroidPrefab;
        [SerializeField] private SpaceObject _smallAsteroidPrefab;
        [SerializeField] private SpaceObject _alienPrefab;
        [SerializeField] private SpaceObject _laserPrefab;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ObjectManager>().FromInstance(_objectManager).AsSingle();

            SpaceObjectQueue bulletQueue = new(_bulletPrefab, _objectManager.transform, _objectManager);
            SpaceObjectQueue asteroidQueue = new(_asteroidPrefab, _objectManager.transform, _objectManager);
            SpaceObjectQueue smallAsteroidQueue = new(_smallAsteroidPrefab, _objectManager.transform, _objectManager);
            SpaceObjectQueue alienQueue = new(_alienPrefab, _objectManager.transform, _objectManager);
            SpaceObjectQueue laserQueue = new(_laserPrefab, _objectManager.transform, _objectManager);

            Container.Bind<SpaceObjectQueue>().WithId("Bullet").FromInstance(bulletQueue).AsTransient();
            Container.Bind<SpaceObjectQueue>().WithId("Asteroid").FromInstance(asteroidQueue).AsTransient();
            Container.Bind<SpaceObjectQueue>().WithId("SmallAsteroid").FromInstance(smallAsteroidQueue).AsTransient();
            Container.Bind<SpaceObjectQueue>().WithId("Alien").FromInstance(alienQueue).AsTransient();
            Container.Bind<SpaceObjectQueue>().WithId("Laser").FromInstance(laserQueue).AsTransient();
        }
    }
}
