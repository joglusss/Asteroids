using Cysharp.Threading.Tasks;

namespace Asteroids.Total
{
    public interface IDataSaver
    {
        public void Save(SaveData data);
        public UniTask<SaveData> Load();
    }
}