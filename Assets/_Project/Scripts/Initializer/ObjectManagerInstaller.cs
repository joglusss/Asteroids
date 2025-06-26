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

            Container.Bind<SpaceObjectQueue>().WithId(SpaceObjectID.Asteroid).AsTransient().WithArguments(_asteroidPrefab, _objectManager);
            Container.Bind<SpaceObjectQueue>().WithId(SpaceObjectID.SmallAsteroid).AsTransient().WithArguments(_smallAsteroidPrefab, _objectManager);
            Container.Bind<SpaceObjectQueue>().WithId(SpaceObjectID.Alien).AsTransient().WithArguments(_alienPrefab, _objectManager);
            Container.Bind<SpaceObjectQueue>().WithId(SpaceObjectID.Laser).AsTransient().WithArguments(_laserPrefab, _objectManager);
            Container.Bind<SpaceObjectQueue>().WithId(SpaceObjectID.Bullet).AsTransient().WithArguments(_bulletPrefab, _objectManager);
        }
    }
}
