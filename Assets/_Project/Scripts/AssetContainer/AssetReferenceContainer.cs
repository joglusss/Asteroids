using Asteroids.Total.Installers;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Asteroids.Asset
{
    public class AssetReferenceContainer<T> : IReadyFlag
    {
        private T _loadedAsset;
        private AssetReference _assetReference;

        public bool IsReady { get; private set; }

        public AssetReferenceContainer(AssetReference assetReference)
        {
            _assetReference = assetReference;
        }
        
        public async Task<T> LoadAsync()
        {
            if (_loadedAsset != null)
                return _loadedAsset;

            var op = await _assetReference.LoadAssetAsync<GameObject>();

            IsReady = true;
            return _loadedAsset = op.GetComponent<T>();
        }
    }
}
