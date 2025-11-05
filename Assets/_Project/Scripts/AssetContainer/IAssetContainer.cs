namespace Asteroids.Asset
{ 
    public interface IAssetContainer<T>
    {
        T LoadSync();
        void Release();
    }
}
