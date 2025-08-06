using System;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;


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
				var json = File.ReadAllText(SavePath);
				data = JsonConvert.DeserializeObject<SaveData>(json);
			}
			
			return data;
		}

		public void Save(SaveData data)
		{
			string json = JsonConvert.SerializeObject(data, Formatting.Indented);
			File.WriteAllText(SavePath, json);
		}
	}
}


