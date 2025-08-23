using System;
using System.Threading.Tasks;
using Zenject;

namespace Asteroids.Asset
{ 
    public interface IAssetContainer<T> where T : UnityEngine.Object
    {
        T LoadedAsset { get; }
        Task<T> LoadAssetAsync();
        T LoadAssetSync();
        void ReleaseAsset();
    }

}
