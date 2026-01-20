using Asteroids.Asset;
using Asteroids.Effect;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Asteroids.Total.Installers
{
    class EffectInstaller : MonoInstaller
    {
        [field: SerializeField] private AssetReference _shootSpark;
        [field: SerializeField] private AssetReference _explode;
        [field: SerializeField] private AssetReference _smallExplode;

        public override void InstallBindings()
        {
            BindEffect(_shootSpark, EffectID.ShotSparks).Forget();
            BindEffect(_explode, EffectID.Explode).Forget();
            BindEffect(_smallExplode, EffectID.SmallExplode).Forget();
        }

        private async UniTask BindEffect(AssetReference address, EffectID id)
        {
            var tempContainer = new AssetReferenceContainer<ParticleSystem>(address);
            Container.BindInterfacesTo<AssetReferenceContainer<ParticleSystem>>()
                .FromInstance(tempContainer);

            var particleSystem = await tempContainer.LoadAsync();

            Container.Bind<ParticleSystem>().WithId(id).FromInstance(particleSystem).AsCached();

            Debug.Log($"Load Asset Effect {id}");
        }
    }
}
