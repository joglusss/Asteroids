using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Asteroids.Asset
{
    public class AssetReferenceContainer<T> : IAssetContainer<T>, IDisposable
    {
        private T loadedAsset;
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
            if (loadedAsset != null)
                return loadedAsset;

            var op = _assetReference.LoadAssetAsync<GameObject>();
            
            return loadedAsset = op.WaitForCompletion().GetComponent<T>();
        }

        public void Release()
        { 
            _assetReference.ReleaseAsset();
        }
    }
}
