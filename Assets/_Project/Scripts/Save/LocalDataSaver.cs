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
				
				var settings = new JsonSerializerSettings();
				settings.Converters.Add(new JsonReactivePropertyConvertor<int>());
				settings.Converters.Add(new JsonObservableHashSetConverter<string>());
				
				data = JsonConvert.DeserializeObject<SaveData>(json, settings);
			}
			
			return data;
		}

		public void Save(SaveData data)
		{
			var settings = new JsonSerializerSettings();
			settings.Converters.Add(new JsonReactivePropertyConvertor<int>());
			settings.Converters.Add(new JsonObservableHashSetConverter<string>());
			
			string json = JsonConvert.SerializeObject(data, Formatting.Indented, settings);
			
			File.WriteAllText(SavePath, json);
		}
	}
}