using System;
using System.IO;
using Cysharp.Threading.Tasks;
using Firebase.RemoteConfig;
using Newtonsoft.Json;
using UnityEngine;
using Zenject;

namespace Asteroids.Total
{ 
    public class RemoteConfig
    {
        public async UniTask<Config> GetConfig(SaveService saveService)
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
                
                saveService.Data.Config = config;
            }
            else
            { 
                config = saveService.Data.Config;
            }
            
            return config; 
        }
    }

}

