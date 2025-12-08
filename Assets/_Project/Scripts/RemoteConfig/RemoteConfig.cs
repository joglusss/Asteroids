using Asteroids.Total.Installers;
using Cysharp.Threading.Tasks;
using Firebase;
using Firebase.RemoteConfig;
using Newtonsoft.Json;
using System;
using UnityEngine;
using Zenject;

namespace Asteroids.Total
{ 
    public class RemoteConfig : IReadyFlag
    {
        public bool IsReady {get; private set;}
        
        [Inject]
        public async UniTask GetConfig(SaveService saveService)
        {
            Debug.Log("Start Config Initializing");
            
            try
            {
                var status = await FirebaseApp.CheckAndFixDependenciesAsync();
                if (status != DependencyStatus.Available)
                {
                    Debug.LogError("Firebase dependencies not available for RemoteConfig. Continuing without remote config");
                    IsReady = true;
                    return;
                }

                if (Application.internetReachability == NetworkReachability.NotReachable)
                {
                    Debug.LogWarning("No internet connectivity detected. Skipping Firebase RemoteConfig fetch.");
                    IsReady = true;
                    return;
                }

                TimeSpan cacheExpiration = TimeSpan.FromSeconds(20);

                var fetchTask = FirebaseRemoteConfig.DefaultInstance.FetchAsync(cacheExpiration);
                await fetchTask;

                if (FirebaseApp.DefaultInstance == null)
                {
                    Debug.LogError("Firebase not initialized before RemoteConfig!");
                    IsReady = true;
                    return;
                }

                if (fetchTask.IsCompletedSuccessfully && FirebaseRemoteConfig.DefaultInstance.Info.LastFetchStatus == LastFetchStatus.Success)
                {
                    await FirebaseRemoteConfig.DefaultInstance.ActivateAsync();

                    string json = FirebaseRemoteConfig.DefaultInstance.GetValue("TotalRemoteConfig").StringValue;
                    Config config = JsonConvert.DeserializeObject<Config>(json);

                    Debug.Log("Config was loaded from cloud");

                    saveService.Config = config;
                }
                else
                {
                    Debug.Log("Config was loaded from JSON");
                }
            }
            catch (Exception ex) {

                Debug.LogWarning($"RemoteConfig fetch failed (continuing without remote config). Exception: {ex}");
            }
            
            Debug.Log("Config Initialized");
            IsReady = true;
        }


    }
}

