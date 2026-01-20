using Asteroids.Objects;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Asteroids.Installers
{ 
    public class LaunchCycleManagerInstaller : MonoInstaller
    {
        [SerializeField] private LaunchCycleManager _objectManager;

        private Transform _spaceObjectContainer;

        public override void InstallBindings()
        {
            _spaceObjectContainer = new GameObject("SpaceObjectContainer").transform;

            BindSpaceObjectQueue(SpaceObjectID.Bullet).Forget();
            BindSpaceObjectQueue(SpaceObjectID.Asteroid).Forget();
            BindSpaceObjectQueue(SpaceObjectID.SmallAsteroid).Forget();
            BindSpaceObjectQueue(SpaceObjectID.Alien).Forget();
            BindSpaceObjectQueue(SpaceObjectID.Laser).Forget();

            Container.BindInterfacesAndSelfTo<LaunchCycleManager>().FromInstance(_objectManager).AsSingle();
        }

        private async UniTask BindSpaceObjectQueue(SpaceObjectID id)
        {
            var gameObject = Container.ResolveId<SpaceObject>(id);

            Container.Bind<SpaceObjectQueue>()
                .WithId(id)
                .AsCached()
                .WithArguments(gameObject, _spaceObjectContainer);
        }
    }
}


