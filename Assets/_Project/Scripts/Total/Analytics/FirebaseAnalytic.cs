using System;
using System.Threading.Tasks;
using Firebase;
using Firebase.Analytics;
using Firebase.Extensions;
using UnityEngine;

namespace Asteroids.Total
{
    public class FirebaseAnalytic : IAnalytic
    {
        private FirebaseAnalytic()
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(OnDependencyStatusReceived);
        }


        public void SendGameStart() => FirebaseAnalytics.LogEvent("StartGame");
        
        public void SendGameStop(int ShootsCount, int LasersCount, int DestroyedAsteroidsCount, int DestroyedAliensCount) =>
            FirebaseAnalytics.LogEvent(
                "StopGame",
                new Parameter[]
                {
                    new Parameter("ShootCount", ShootsCount),
                    new Parameter("LasersCount",LasersCount),
                    new Parameter("DestroyedAsteroidsCount",DestroyedAsteroidsCount),
                    new Parameter("DestroyedAliensCount",DestroyedAliensCount)
                });

        public void SendLaserUsed() => FirebaseAnalytics.LogEvent("Laser");

        private void OnDependencyStatusReceived(Task<DependencyStatus> task)
        {
            try
            {
                if (!task.IsCompletedSuccessfully)
                    throw new Exception("Firebase initialize unsuccessfully", task.Exception);

                var status = task.Result;
                if (status != DependencyStatus.Available)
                    throw new Exception($"Firebase initialize unsuccessfully {status}");
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}
