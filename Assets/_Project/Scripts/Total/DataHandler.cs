using R3;
using System.IO;
using UnityEditor.Overlays;
using UnityEngine;
using Zenject;

namespace Asteroids.Total
{
    public class DataHandler : IInitializable, ILateDisposable
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
        private string SavePath => Path.Combine(Application.dataPath, "Data.json");

        public void Initialize()
        {
            SaveData Data = new SaveData();

            if (File.Exists(SavePath))
            {
                string json = File.ReadAllText(SavePath);
                Data = JsonUtility.FromJson<SaveData>(json);
            }

            BestScore = Data.BestScore;
            LastScore = Data.LastScore;
        }

        public void LateDispose() 
        {
            string json = JsonUtility.ToJson(new SaveData()
            {
                BestScore = BestScore,
                LastScore = LastScore,
            });
            File.WriteAllText(SavePath, json);
        }
    }
}
