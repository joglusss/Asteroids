using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Asteroids.Asset
{
    public class AssetReferenceContainer<T> : IDisposable
    {
        private T _loadedAsset;
        private AssetReference _assetReference;
        
        public AssetReferenceContainer(AssetReference assetReference)
        {
            _assetReference = assetReference;
        }

        public void Dispose()
        {
            Release();
        }
        
        public T LoadSync()
        {
            if (_loadedAsset != null)
                return _loadedAsset;

            var op = _assetReference.LoadAssetAsync<GameObject>();
            
            return _loadedAsset = op.WaitForCompletion().GetComponent<T>();
        }

        public void Release()
        { 
            _assetReference.ReleaseAsset();
        }
    }
}
