using R3;
using System;
using System.IO;
using UnityEngine;
using Zenject;

namespace Asteroids.Total
{
    public class SaveManager : IInitializable, IDisposable
    {
        public Observable<int> BestScoreSub => _bestScore;
        public Observable<int> LastScoreSub => _lastScore;

        public int BestScore
        {
            get => _bestScore.Value;
            private set => _bestScore.Value = value;
        }
        public int LastScore
        {
            get => _lastScore.Value;
            set
            {
                _bestScore.Value = Mathf.Max(value, BestScore);
                _lastScore.Value = value;
            }
        }

        private readonly ReactiveProperty<int> _bestScore = new();
        private readonly ReactiveProperty<int> _lastScore = new();
        private IDataSaver _dataSaver;

        [Inject]
        private void Construct(IDataSaver dataSaver)
        { 
            _dataSaver = dataSaver;
        }

        public void Initialize()
        {
            SaveData Data = _dataSaver.Load();

            BestScore = Data.BestScore;
            LastScore = Data.LastScore;
        }

        public void Dispose() 
        {
            _dataSaver.Save(new SaveData()
            {
                BestScore = BestScore,
                LastScore = LastScore,
            });  
        }
    }
}
