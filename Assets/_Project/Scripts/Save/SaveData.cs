using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using ObservableCollections;
using R3;
using Unity.VisualScripting;
using UnityEngine;

namespace Asteroids.Total
{
	public class SaveData
	{
        [JsonProperty] public DateTime DateSaving { get; set; } = new();
        [JsonProperty] public int BestScore { get; set; }
        [JsonProperty] public int LastScore { get; set; }
        [JsonProperty] public HashSet<string> PurchasedProduct { get; set; } = new();
		[JsonProperty] public Config Config = new();

		public void Rewrite(SaveData newData)
		{
			PurchasedProduct.Clear();
			PurchasedProduct.AddRange(newData.PurchasedProduct);

			Config = newData.Config;
			DateSaving = newData.DateSaving;
			BestScore = newData.BestScore;
			LastScore = newData.LastScore;
        }
	}
}
