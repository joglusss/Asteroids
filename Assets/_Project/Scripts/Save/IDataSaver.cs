using Cysharp.Threading.Tasks;

namespace Asteroids.Total
{
    public interface IDataSaver
    {
        public UniTask Save(SaveData data);
        public UniTask<SaveData> Load();
    }
}