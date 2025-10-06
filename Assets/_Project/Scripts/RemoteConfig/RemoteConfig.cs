using System;
using System.IO;
using Cysharp.Threading.Tasks;
using Firebase.RemoteConfig;
using Newtonsoft.Json;
using UnityEngine;

namespace Asteroids.Total
{ 
    public class RemoteConfig
    {
        private string SavePath => Path.Combine(Application.dataPath, "LastFetchConfig.json");
        
        public async UniTask<Config> GetConfig()
        {
            TimeSpan cacheExpiration = TimeSpan.FromSeconds(30);
            var fetchTask = FirebaseRemoteConfig.DefaultInstance.FetchAsync(cacheExpiration);
            await fetchTask.AsUniTask();

            Config config;
            
            if (fetchTask.IsCompletedSuccessfully && FirebaseRemoteConfig.DefaultInstance.Info.LastFetchStatus == LastFetchStatus.Success)
            {
                await FirebaseRemoteConfig.DefaultInstance.ActivateAsync().AsUniTask();
                
                string json = FirebaseRemoteConfig.DefaultInstance.GetValue("TotalRemoteConfig").StringValue;
                config = JsonConvert.DeserializeObject<Config>(json);
                File.WriteAllText(SavePath, json);
            }
            else
            { 
                Debug.LogWarning("Failed to fetch remote config.");
                
                var json = File.ReadAllText(SavePath);
                config = JsonConvert.DeserializeObject<Config>(json);
            }
            
            return config; 
        }
    }

}

