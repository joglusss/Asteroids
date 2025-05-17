using System.IO;
using System;
using UnityEngine;
using Asteroids.SceneManage;

namespace Asteroids.Score
{
    public class ScoreManager : MonoBehaviour, IInitialize
    {
        public event Action<int> LastScoreChanged;

        public int BestScore => _saveData.BestScore;
        public int LastScore => _saveData.LastScore;

        private SaveData _saveData;
        private string SavePath => Path.Combine(Application.dataPath, "saveScore.json");

        public void Initialize(DependencyContainer dependencyContainer = null) => Load();

        private void OnDestroy() => Save();

        public void AddScore(int value)
        {
            if (value < 0)
                return;

            _saveData.LastScore += value;
            LastScoreChanged?.Invoke(value);
        }

        public void ResetLastScore()
        {
            _saveData.LastScore = 0;
        }

        private class SaveData
        {
            public int BestScore;
            public int LastScore;
        }

        private void Save()
        {
            _saveData.BestScore = Mathf.Max(_saveData.LastScore,_saveData.BestScore);
            string json = JsonUtility.ToJson(_saveData);
            File.WriteAllText(SavePath, json);
        }

        private void Load()
        { 
            if(!File.Exists(SavePath))
            {
                _saveData = new SaveData();
                return;
            }

            string json = File.ReadAllText(SavePath);
            _saveData = JsonUtility.FromJson<SaveData>(json);
        }

    }
}