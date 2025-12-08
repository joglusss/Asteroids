using UnityEngine;
using Asteroids.Helpers;
using Zenject;
using Asteroids.Ship;
using Asteroids.Total;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace Asteroids.Objects
{
    public class LaunchCycleManager : MonoBehaviour, IInitializable
    {
        private ShipStatViewModel _shipStatVM;
        private BorderSetting _borderSetting;
        private SpaceObjectQueue _asteroidQueue;
        private SpaceObjectQueue _alienQueue;
        private Config _config;

        [Inject]
        private void Construct(
            ShipStatViewModel shipStatVM,
            BorderSetting borderSetting,
            SaveService saveService,
            [Inject(Id = SpaceObjectID.Asteroid)] SpaceObjectQueue asteroid,
            [Inject(Id = SpaceObjectID.Alien)] SpaceObjectQueue alien)
        {
            _config = saveService.DataState.Config;
            _asteroidQueue = asteroid;
            _borderSetting =  borderSetting;
            _alienQueue = alien;
            _shipStatVM = shipStatVM;
        }

        public void Initialize()
        {
            LaunchCycle(_asteroidQueue, _config.Asteroids, this.GetCancellationTokenOnDestroy());
            LaunchCycle(_alienQueue, _config.Alien, this.GetCancellationTokenOnDestroy());
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        private void LaunchObject(SpaceObject spaceObject)
        {
            Vector2 position = _borderSetting.Size + new Vector2(UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(-1.0f, 1.0f)) * _borderSetting.Size.magnitude;
            position = GameMath.TeleportToBorder(position, _borderSetting.Center, _borderSetting.Points);
            spaceObject.Launch(position, new Vector2(UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(-1.0f, 1.0f)));
        }

        private async void LaunchCycle(SpaceObjectQueue objQueue, LaunchCycleSetting setting, CancellationToken token)
        {
            for (int i = 0; i < setting.StartObjectCount; i++)
            {
                LaunchObject(objQueue.DrawObject());
            }

            await UniTask.Yield();
            
            while (enabled)
            {
                if(_shipStatVM.LifeStatus.CurrentValue)
                    LaunchObject(objQueue.DrawObject());

                try
                {
                    await UniTask.WaitForSeconds(UnityEngine.Random.Range(setting.MinTime, setting.MaxTime), cancellationToken: token);
                }
                catch
                {
                    return;
                }
            }
        }
    }
}
