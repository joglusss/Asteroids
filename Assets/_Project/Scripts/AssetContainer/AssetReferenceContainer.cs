using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Asteroids.Asset
{
    public class AssetReferenceContainer<T> : IAssetContainer<T>, IDisposable where T : UnityEngine.Object
    {
        public T LoadedAsset { get; private set; }

        private AssetReferenceT<T> _assetReference;
        private UniTaskCompletionSource<T> _loadTaskSource;
        
        public AssetReferenceContainer(AssetReferenceT<T> assetReference)
        {
            _assetReference = assetReference;
        }

        public void Dispose()
        {
            ReleaseAsset();
        }

        public async UniTask<T> LoadAssetAsync()
        { 
            if (LoadedAsset != null)
                return LoadedAsset;
            if(_loadTaskSource != null)
                return  await _loadTaskSource.Task;

            _loadTaskSource = new();

            _assetReference.LoadAssetAsync<T>().Completed += handle =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    Debug.Log($"Asset for {handle.DebugName} failed to load.");
                    LoadedAsset = handle.Result;
                    _loadTaskSource.TrySetResult(handle.Result);
                }
                else
                {
                    Debug.LogError($"Asset for {handle.DebugName} failed to load.");
                }
            };
            
            return await _loadTaskSource.Task;
        }
        
        public T LoadAssetSync()
        {
            if (LoadedAsset != null)
                return LoadedAsset;
                
            var op = _assetReference.LoadAssetAsync();
            
            return op.WaitForCompletion();
        }

        public void ReleaseAsset()
        { 
            _assetReference.ReleaseAsset();
        }
    }
}
