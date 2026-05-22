using UnityEngine;
using Asteroids.Helpers;
using Zenject;
using Asteroids.Ship;
using Asteroids.Total;
using Cysharp.Threading.Tasks;
using System.Threading;
using System;
using R3;

namespace Asteroids.Objects
{
    public class LaunchCycleManager : IInitializable, IDisposable
    {
        private CancellationTokenSource _cst = new();
        private ShipStatViewModel _shipStatVM;
        private BorderSetting _borderSetting;
        private SpaceObjectPool _asteroidPool;
        private SpaceObjectPool _alienPool;
        private Config _config;

        [Inject]
        private void Construct(
            ShipStatViewModel shipStatVM,
            BorderSetting borderSetting,
            SaveService saveService,
            [Inject(Id = SpaceObjectID.Asteroid)] SpaceObjectPool asteroid,
            [Inject(Id = SpaceObjectID.Alien)] SpaceObjectPool alien)
        {
            _config = saveService.DataState.Config;
            _asteroidPool = asteroid;
            _borderSetting =  borderSetting;
            _alienPool = alien;
            _shipStatVM = shipStatVM;
        }

        public void Initialize()
        {
            LaunchCycle(_asteroidPool, _config.Asteroids, _cst.Token).Forget();
            LaunchCycle(_alienPool, _config.Alien, _cst.Token).Forget();
        }

        public void Dispose()
        {
            _cst.Cancel();
            _cst.Dispose();
        }

        private void LaunchObject(SpaceObject spaceObject)
        {
            Vector2 position = _borderSetting.Size + new Vector2(UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(-1.0f, 1.0f)) * _borderSetting.Size.magnitude;
            position = GameMath.TeleportToBorder(position, _borderSetting.Center, _borderSetting.Points);
            spaceObject.Launch(position, new Vector2(UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(-1.0f, 1.0f)));
        }

        private async UniTask LaunchCycle(SpaceObjectPool objPool, LaunchCycleSetting setting, CancellationToken token)
        {
            for (int i = 0; i < setting.StartObjectCount; i++)
            {
                LaunchObject(objPool.DrawObject());
            }

            await UniTask.Yield();
            
            while (true)
            {
                if(_shipStatVM.LifeStatus.CurrentValue)
                    LaunchObject(objPool.DrawObject());

                await UniTask.WaitForSeconds(UnityEngine.Random.Range(setting.MinTime, setting.MaxTime), cancellationToken: token);
            }
        }


    }
}
