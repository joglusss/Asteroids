using System;
using System.IO;
using Asteroids.Total.Installers;
using Cysharp.Threading.Tasks;
using Firebase.RemoteConfig;
using Newtonsoft.Json;
using UnityEngine;
using Zenject;

namespace Asteroids.Total
{ 
    public class RemoteConfig : IReadyFlag
    {
        public bool IsReady {get; private set;}
        
        [Inject]
        public async UniTaskVoid GetConfig(SaveService saveService)
        {
             Debug.Log("Start Config Initializing");
        
            TimeSpan cacheExpiration = TimeSpan.FromSeconds(30);
            var fetchTask = FirebaseRemoteConfig.DefaultInstance.FetchAsync(cacheExpiration);
            await fetchTask.AsUniTask();
            
            if (fetchTask.IsCompletedSuccessfully && FirebaseRemoteConfig.DefaultInstance.Info.LastFetchStatus == LastFetchStatus.Success)
            {
                await FirebaseRemoteConfig.DefaultInstance.ActivateAsync().AsUniTask();
                
                string json = FirebaseRemoteConfig.DefaultInstance.GetValue("TotalRemoteConfig").StringValue;
                Config config = JsonConvert.DeserializeObject<Config>(json);

                Debug.Log("Config was loaded from cloud");
                
                saveService.Config = config;
            }
            else
            { 
                Debug.Log("Config was loaded from JSON");
            }

            Debug.Log("Config Initialized");
            IsReady = true;
        }


    }
}

