using Newtonsoft.Json;
using ObservableCollections;
using R3;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

namespace Asteroids.Total
{
    public class SaveDataState : IDisposable
    {
        public ValidatedReactive<int> LastScore { get; private set; }
        public ObservableHashSet<string> PurchasedProduct { get; private set; }
        public ReadOnlyReactiveProperty<int> BestScore => _bestScore;
        public Config Config { get => _saveData.Config; }

        private SaveData _saveData;
        private CompositeDisposable _disposales;
        private ReactiveProperty<int> _bestScore = new();

        public void Initialize(SaveData saveData)
        {
            _disposales?.Dispose();
            _disposales = new CompositeDisposable();

            _saveData = saveData;

            LastScore = new ValidatedReactive<int>(
                    saveData.LastScore,
                    x => Mathf.Max(x,0));
            LastScore.Subscribe(x =>
            {
                _bestScore.Value = Mathf.Max(x, _bestScore.Value);
                _saveData.LastScore = x;
                _saveData.BestScore = _bestScore.Value;
            }).AddTo(_disposales);

            PurchasedProduct = new ObservableHashSet<string>(_saveData.PurchasedProduct);
            PurchasedProduct.ObserveAdd()
                .Subscribe(x => saveData.PurchasedProduct.Add(x.Value))
                .AddTo(_disposales);
            PurchasedProduct.ObserveRemove()
                .Subscribe(x => saveData.PurchasedProduct.Remove(x.Value))
                .AddTo(_disposales);
        }

        public void Dispose()
        {
            _disposales.Dispose();
        }
    }
}