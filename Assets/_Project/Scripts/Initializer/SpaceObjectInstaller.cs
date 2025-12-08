using Asteroids.Asset;
using Asteroids.Objects;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Asteroids.Installers
{ 
    public class SpaceObjectInstaller : MonoInstaller
    {
        [SerializeField] private AssetReference _bulletAddress;
        [SerializeField] private AssetReference _asteroidAddress;
        [SerializeField] private AssetReference _smallAsteroidAddress;
        [SerializeField] private AssetReference _alienAddress;
        [SerializeField] private AssetReference _laserAddress;

        private Transform _spaceObjectContainer;
        
        public override void InstallBindings()
        {
            _spaceObjectContainer = new GameObject("SpaceObjectContainer").transform;
        
            BindSpaceObject(_bulletAddress, SpaceObjectID.Bullet);
            BindSpaceObject(_asteroidAddress, SpaceObjectID.Asteroid);
            BindSpaceObject(_smallAsteroidAddress, SpaceObjectID.SmallAsteroid);
            BindSpaceObject(_alienAddress, SpaceObjectID.Alien);
            BindSpaceObject(_laserAddress, SpaceObjectID.Laser);
        }
        
        private void BindSpaceObject(AssetReference address, SpaceObjectID ID)
        { 
            var tempContainer = new AssetReferenceContainer<SpaceObject>(address);
            Container.Bind<SpaceObjectQueue>()
                .WithId(ID)
                .AsCached()
                .WithArguments(tempContainer.LoadSync(),_spaceObjectContainer);
            Container.BindInterfacesTo<AssetReferenceContainer<SpaceObject>>()
                .FromInstance(tempContainer);
        }
    }
}


