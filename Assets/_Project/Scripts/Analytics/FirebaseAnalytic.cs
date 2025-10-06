using System;
using System.Threading.Tasks;
using Firebase;
using Firebase.Analytics;
using Firebase.Extensions;
using UnityEngine;

namespace Asteroids.Total
{
    public class FirebaseAnalytic : IAnalyticsService
    {
        private FirebaseAnalytic()
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(OnDependencyStatusReceived);
        }

        public void SendGameStart() => FirebaseAnalytics.LogEvent("StartGame");
        
        public void SendGameStop(StopGameLogParameters parameters) =>
            FirebaseAnalytics.LogEvent(
                "StopGame",
                new Parameter[]
                {
                    new Parameter("ShootCount", parameters.ShootsCount),
                    new Parameter("LasersCount",parameters.LasersCount),
                    new Parameter("DestroyedAsteroidsCount",parameters.DestroyedAsteroidsCount),
                    new Parameter("DestroyedAliensCount",parameters.DestroyedAliensCount)
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
