namespace Asteroids.Total
{
    public interface IDataSaver
    {
        public void Save(SaveData data);
        public SaveData Load();
    }
}