namespace Asteroids.Asset
{ 
    public interface IAssetContainer<T> //where T : UnityEngine.Object
    {
        T LoadSync();
        void Release();
    }
}
