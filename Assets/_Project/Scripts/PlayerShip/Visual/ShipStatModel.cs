using Asteroids.Ads;
using Asteroids.SceneManage;
using Asteroids.Total;
using Cysharp.Threading.Tasks;
using R3;
using System;
using System.Threading;
using UnityEngine;
using Zenject;

namespace Asteroids.Ship
{
	public class ShipStatModel : IDisposable
	{
		public readonly ReactiveProperty<int> Health = new();
		public readonly ReactiveProperty<bool> Immortality = new(false);
		public readonly ReactiveProperty<Vector2> Coordinates = new();
		public readonly ReactiveProperty<float> Angle = new();
		public readonly ReactiveProperty<float> Speed = new();
		public readonly ReactiveProperty<int> LaserCount = new();
		public readonly ReactiveProperty<float> LaserCooldown = new();
		public readonly ReactiveProperty<bool> LifeStatus = new(true);

		private IAdsService _adsService;
		private ShipAnimationControl _shipAnimationControl;
		private Config _config;
		private SceneService _sceneContainerHandler;
		private bool _recoveryLaserCountFlag;
		private bool _isFirstDeath = true;
		private CancellationTokenSource _cts = new();

		[Inject]
		public void Construct(SceneService sceneContainerHandler, ShipAnimationControl shipAnimationControl, SaveService saveService, IAdsService adsService)
		{
			_sceneContainerHandler = sceneContainerHandler;
		
			_shipAnimationControl = shipAnimationControl;
			_config = saveService.DataState.Config;

			Health.Value = _config.MaxHpCount;
			LaserCount.Value = _config.MaxLaserCount;

			_adsService = adsService;
		}
		
		
        public void Dispose()
        {
            _cts.Cancel();
            _cts.Dispose();
        }

		public void AddHealth(int value)
		{ 
			if (Immortality.Value && value < 0) return;
			
			Health.Value =  Math.Clamp(Health.Value + value, 0, _config.MaxHpCount);

			StartImmortalityFrame(_config.ImmortalityTime, _cts.Token).Forget();			
			
			if (Health.Value <= 0)
			{
				if (!_isFirstDeath)
				{ 
					GiveUp().Forget();
					return;
				}
					
				LifeStatus.Value = false;
				return;
			}
			else
				LifeStatus.Value = true;
		}

		public async UniTask Resurrect() 
		{
			if (_isFirstDeath)
			{ 
				_isFirstDeath = false;
				
				AdStat abc = await _adsService.ShowRewardedAd(_cts.Token);

				if (abc == AdStat.Ended)
				{ 
					AddHealth(_config.MaxHpCount);
					return;
				}
			}

			await _shipAnimationControl.Death(_cts.Token);
			_sceneContainerHandler.GoToMenu();
		}

		public async UniTask GiveUp() 
		{
			await _shipAnimationControl.Death(_cts.Token);
		
			await _adsService.ShowInterstitialAd(_cts.Token);
			
			_sceneContainerHandler.GoToMenu();
		}
		
		public void AddLaserCount(int value)
		{
			LaserCount.Value = Mathf.Clamp(LaserCount.Value + value, 0, _config.MaxLaserCount);
			
			if(!_recoveryLaserCountFlag)
				RecoveryLaserCount();
		}

		public void SetCoordinates(Vector2 value) => Coordinates.Value = value;

		public void SetAngle(float value) => Angle.Value = value;
		
		public void SetSpeed(float value) => Speed.Value = value;

		private async void RecoveryLaserCount()
		{
			_recoveryLaserCountFlag = true;
			
			LaserCooldown.Value = _config.LaserCooldown;
			while (LaserCount.Value < _config.MaxLaserCount)
			{ 
				while (LaserCooldown.Value > 0.0f)
				{ 	
					await UniTask.Delay(100, cancellationToken: _cts.Token);
					LaserCooldown.Value = Mathf.Clamp(LaserCooldown.Value - 0.1f, 0.0f, float.MaxValue);
				}

				LaserCount.Value = Mathf.Clamp(LaserCount.Value + 1, 0, _config.MaxLaserCount);
			}
			
			_recoveryLaserCountFlag = false;
		}
		
		private async UniTaskVoid StartImmortalityFrame(float time, CancellationToken token)
		{
			Immortality.Value = true;
			_shipAnimationControl.SwitchBlinking(true);

			while (time > 0.0f)
			{
				await UniTask.Delay(1000, cancellationToken: token).SuppressCancellationThrow();
				time = Mathf.Clamp(time - 1.0f, 0.0f, float.MaxValue);
			}
			
			Immortality.Value = false;
			_shipAnimationControl.SwitchBlinking(false);
		}
    }
}
