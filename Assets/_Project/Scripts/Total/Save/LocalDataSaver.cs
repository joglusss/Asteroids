using System.IO;
using UnityEngine;

namespace Asteroids.Total 
{
    public class LocalDataSaver : IDataSaver
    {
        private string SavePath => Path.Combine(Application.dataPath, "Data.json");

        public SaveData Load()
        {
            SaveData data = new SaveData();

            if (File.Exists(SavePath))
            {
                string json = File.ReadAllText(SavePath);
                data = JsonUtility.FromJson<SaveData>(json);
            }

            return data;
        }

        public void Save(SaveData data)
        {
            string json = JsonUtility.ToJson(data);
            File.WriteAllText(SavePath, json);
        }
    }
}


