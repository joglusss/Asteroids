using Asteroids.Objects;
using UnityEngine;
using Zenject;
using UnityEngine.AddressableAssets;
using Asteroids.Asset;
using UnityEditor.Rendering;
using System;

namespace Asteroids.Installers
{
    public class ObjectManagerInstaller : MonoInstaller
    {
        [SerializeField] private ObjectManager _objectManager;
        [SerializeField] private AssetReferenceT<GameObject> _bulletAddress;
        [SerializeField] private AssetReferenceT<GameObject> _asteroidAddress;
        [SerializeField] private AssetReferenceT<GameObject> _smallAsteroidAddress;
        [SerializeField] private AssetReferenceT<GameObject> _alienAddress;
        [SerializeField] private AssetReferenceT<GameObject> _laserAddress;

        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ObjectManager>().FromInstance(_objectManager).AsSingle();

            BindSpaceObject(_bulletAddress, SpaceObjectID.Bullet);
            BindSpaceObject(_asteroidAddress, SpaceObjectID.Asteroid);
            BindSpaceObject(_smallAsteroidAddress, SpaceObjectID.SmallAsteroid);
            BindSpaceObject(_alienAddress, SpaceObjectID.Alien);
            BindSpaceObject(_laserAddress, SpaceObjectID.Laser);
        }

        private void BindSpaceObject(AssetReferenceT<GameObject> address, SpaceObjectID ID)
        { 
            var tempContainer = new AssetReferenceContainer<GameObject>(address);
            Container.Bind<IAssetContainer<GameObject>>()
                .WithId(ID)
                .To<AssetReferenceContainer<GameObject>>()
                .FromInstance(tempContainer)
                .AsTransient()
                .NonLazy();
            Container.BindInterfacesTo<AssetReferenceContainer<GameObject>>()
                .FromInstance(tempContainer);
        }
    }
}
