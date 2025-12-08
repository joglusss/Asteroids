using Asteroids.Total.Installers;
using Cysharp.Threading.Tasks;
using Firebase;
using Firebase.Analytics;
using System;
using UnityEngine;
using Zenject;

namespace Asteroids.Total
{
    public class FirebaseAnalytic : IAnalyticsService, IInitializable, IReadyFlag
    {
        public bool IsReady {get; private set; }

        public void Initialize() => InitializeAsync().Forget();


        public void SendGameStart() { if (FirebaseApp.DefaultInstance != null) FirebaseAnalytics.LogEvent("StartGame"); }

        public void SendGameStop(StopGameLogParameters parameters)
        {
            if (FirebaseApp.DefaultInstance != null)
                FirebaseAnalytics.LogEvent(
                    "StopGame",
                    new Parameter[]
                    {
                        new Parameter("ShootCount", parameters.ShootsCount),
                        new Parameter("LasersCount",parameters.LasersCount),
                        new Parameter("DestroyedAsteroidsCount",parameters.DestroyedAsteroidsCount),
                        new Parameter("DestroyedAliensCount",parameters.DestroyedAliensCount)
                    });
        }

        public void SendLaserUsed() { if (FirebaseApp.DefaultInstance != null) FirebaseAnalytics.LogEvent("Laser"); }
        private async UniTask InitializeAsync()
        {
            Debug.Log("Start FirebaseAnalytic Initializing");

            try
            {
                if (Application.internetReachability == NetworkReachability.NotReachable)
                {
                    Debug.LogWarning("No internet connectivity detected. Skipping Firebase initialization.");
                    IsReady = true;
                    return;
                }

                var status = await FirebaseApp.CheckAndFixDependenciesAsync();
                if (status != DependencyStatus.Available)
                {
                    IsReady = true;
                    return;
                }

                if (FirebaseApp.DefaultInstance == null)
                {
                    Debug.LogError("Firebase not initialized before RemoteConfig!");
                    IsReady = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"FirebaseAnalytic initialization failed (continuing without Firebase). Exception: {ex}");
            }

            IsReady = true;
        }
    }
}
