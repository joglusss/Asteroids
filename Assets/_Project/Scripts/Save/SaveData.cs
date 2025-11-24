using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using ObservableCollections;
using R3;
using UnityEngine;

namespace Asteroids.Total
{
	public class SaveData
	{
		[JsonIgnore] public ReadOnlyReactiveProperty<int> BestScoreSubscribe => _bestScore;
		[JsonIgnore] public ReadOnlyReactiveProperty<int> LastScoreSubscribe => _lastScore;
		[JsonProperty] public ObservableHashSet<string> PurchasedProduct { get; set; } = new();
		[JsonProperty] public Config Config = new();
		[JsonProperty] public DateTime DateSaving = new();

		[JsonProperty] private ReactiveProperty<int> _bestScore = new();
		[JsonProperty] private ReactiveProperty<int> _lastScore = new();

		[JsonIgnore]
		public int BestScore
		{
			get => _bestScore.Value;
			private set => _bestScore.Value = value;
		}
		[JsonIgnore]
		public int LastScore
		{
			get => _lastScore.Value;
			set
			{
				_bestScore.Value = Mathf.Max(value, BestScore);
				_lastScore.Value = value;
			}
		}

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
