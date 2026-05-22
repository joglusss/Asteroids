using Asteroids.Objects;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Asteroids.Installers
{ 
    public class LaunchCycleManagerInstaller : MonoInstaller
    {
        private Transform _spaceObjectContainer;

        public override void InstallBindings()
        {
            _spaceObjectContainer = new GameObject("SpaceObjectContainer").transform;

            BindSpaceObjectQueue(SpaceObjectID.Bullet);
            BindSpaceObjectQueue(SpaceObjectID.Asteroid);
            BindSpaceObjectQueue(SpaceObjectID.SmallAsteroid);
            BindSpaceObjectQueue(SpaceObjectID.Alien);
            BindSpaceObjectQueue(SpaceObjectID.Laser);

            Container.BindInterfacesAndSelfTo<LaunchCycleManager>().AsSingle();
        }

        private void BindSpaceObjectQueue(SpaceObjectID id)
        {
            var gameObject = Container.ResolveId<SpaceObject>(id);

            Container.Bind<SpaceObjectPool>()
                .WithId(id)
                .AsCached()
                .WithArguments(gameObject, _spaceObjectContainer);
        }
    }
}


