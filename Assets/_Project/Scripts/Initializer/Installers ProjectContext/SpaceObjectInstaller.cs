using Asteroids.Asset;
using Asteroids.Objects;
using Cysharp.Threading.Tasks;
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

        public override void InstallBindings()
        {
            BindSpaceObject(_bulletAddress, SpaceObjectID.Bullet).Forget();
            BindSpaceObject(_asteroidAddress, SpaceObjectID.Asteroid).Forget();
            BindSpaceObject(_smallAsteroidAddress, SpaceObjectID.SmallAsteroid).Forget();
            BindSpaceObject(_alienAddress, SpaceObjectID.Alien).Forget();
            BindSpaceObject(_laserAddress, SpaceObjectID.Laser).Forget();
        }
        
        private async UniTask BindSpaceObject(AssetReference address, SpaceObjectID id)
        { 
            var tempContainer = new AssetReferenceContainer<SpaceObject>(address);
            Container.BindInterfacesTo<AssetReferenceContainer<SpaceObject>>()
                .FromInstance(tempContainer);

            var spaceObject = await tempContainer.LoadAsync();

            Container.Bind<SpaceObject>().WithId(id).FromInstance(spaceObject).AsCached();

            Debug.Log($"Load Asset Space Object {id}");
        }
    }
}


