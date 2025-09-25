using Cysharp.Threading.Tasks;

namespace Asteroids.Asset
{ 
    public interface IAssetContainer<T> where T : UnityEngine.Object
    {
        T LoadedAsset { get; }
        UniTask<T> LoadAssetAsync();
        T LoadAssetSync();
        void ReleaseAsset();
    }

}
