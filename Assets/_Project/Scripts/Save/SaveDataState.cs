using ObservableCollections;
using R3;
using System;
using UnityEngine;

namespace Asteroids.Total
{
    public class SaveDataState : IDisposable
    {
        public ValidatedReactive<int> LastScore { get; private set; }
        public ObservableHashSet<string> PurchasedProduct { get; private set; }
        public ReadOnlyReactiveProperty<int> BestScore => _bestScore;
        public Config Config { get => _saveData.Config; }

        private SaveData _saveData;
        private CompositeDisposable _compositeDisposable;
        private ReactiveProperty<int> _bestScore = new();

        public void Initialize(SaveData saveData)
        {
            _compositeDisposable?.Dispose();
            _compositeDisposable = new CompositeDisposable();

            _saveData = saveData;

            LastScore = new ValidatedReactive<int>(
                    saveData.LastScore,
                    x => Mathf.Max(x,0));

            LastScore.Subscribe(x =>
            {
                _bestScore.Value = Mathf.Max(x, _bestScore.Value);
                _saveData.LastScore = x;
                _saveData.BestScore = _bestScore.Value;
            }).AddTo(_compositeDisposable);

            PurchasedProduct = new ObservableHashSet<string>(_saveData.PurchasedProduct);

            PurchasedProduct.ObserveAdd()
                .Subscribe(x => saveData.PurchasedProduct.Add(x.Value))
                .AddTo(_compositeDisposable);

            PurchasedProduct.ObserveRemove()
                .Subscribe(x => saveData.PurchasedProduct.Remove(x.Value))
                .AddTo(_compositeDisposable);
        }

        public void Dispose()
        {
            _compositeDisposable.Dispose();
        }
    }
}