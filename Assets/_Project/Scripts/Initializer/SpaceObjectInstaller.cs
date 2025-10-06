using System;
using Asteroids.Asset;
using Asteroids.Objects;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Asteroids.Installers
{ 
    public class SpaceObjectInstaller : MonoInstaller
    {
        [SerializeField] private AssetReferenceT<GameObject> _bulletAddress;
        [SerializeField] private AssetReferenceT<GameObject> _asteroidAddress;
        [SerializeField] private AssetReferenceT<GameObject> _smallAsteroidAddress;
        [SerializeField] private AssetReferenceT<GameObject> _alienAddress;
        [SerializeField] private AssetReferenceT<GameObject> _laserAddress;

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
        
        private void BindSpaceObject(AssetReferenceT<GameObject> address, SpaceObjectID ID)
        { 
            var tempContainer = new AssetReferenceContainer<GameObject>(address);
            Container.Bind<SpaceObjectQueue>()
                .WithId(ID)
                .AsCached()
                .WithArguments(tempContainer.LoadSync().GetComponent<SpaceObject>(),_spaceObjectContainer);
            Container.BindInterfacesTo<AssetReferenceContainer<GameObject>>()
                .FromInstance(tempContainer);
        }
    }
}


