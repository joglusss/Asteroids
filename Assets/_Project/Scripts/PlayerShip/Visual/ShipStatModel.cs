using Asteroids.Visual;
using R3;
using System;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Asteroids.Ship
{
	public class ShipStatModel
	{
		public readonly ReactiveProperty<int> _health = new();
		public readonly ReactiveProperty<bool> _immortality = new(false);
		public readonly ReactiveProperty<Vector2> _coordinates = new();
		public readonly ReactiveProperty<float> _angle = new();
		public readonly ReactiveProperty<float> _speed = new();
		public readonly ReactiveProperty<int> _laserCount = new();
		public readonly ReactiveProperty<float> _laserCooldown = new();
		public readonly ReactiveProperty<bool> _lifeStatus = new(true);
		
		private ShipAnimationControl _shipAnimationControl;
		private ShipStatPreference _shipStatPreference;
		private bool RecoveryLaserCountFlag;

		[Inject]
		public void Construct(ShipAnimationControl shipAnimationControl, ShipStatPreference shipStatPreference)
		{
			_shipAnimationControl = shipAnimationControl;
			_shipStatPreference = shipStatPreference;

			_health.Value = shipStatPreference.MaxHealth;
			_laserCount.Value = shipStatPreference.MaxLaserCount;
		}

		public void AddHealth(int value)
		{ 
			if (_immortality.Value) return;
			_health.Value = Math.Clamp(_health.Value + value, 0, _shipStatPreference.MaxHealth);

			StartImmortalityFrame(_shipStatPreference.ImmortalityTime);

			if (_health.Value <= 0) Death();
		}

		public void AddLaserCount(int value)
		{
			_laserCount.Value = Mathf.Clamp(_laserCount.Value + value, 0, _shipStatPreference.MaxLaserCount);
			
			if(!RecoveryLaserCountFlag)
				RecoveryLaserCount();
		}

		public void SetCoordinates(Vector2 value) => _coordinates.Value = value;

		public void SetAngle(float value) => _angle.Value = value;
		
		public void SetSpeed(float value) => _speed.Value = value;

		private async void RecoveryLaserCount()
		{
			RecoveryLaserCountFlag = true;
			
			_laserCooldown.Value = _shipStatPreference.LaserCooldown;
			while (_laserCount.Value < _shipStatPreference.MaxLaserCount)
			{ 
				while (_laserCooldown.Value > 0.0f)
				{ 	
					await Task.Delay(100);
					_laserCooldown.Value = Mathf.Clamp(_laserCooldown.Value - 0.1f, 0.0f, float.MaxValue);
				}

				_laserCount.Value = Mathf.Clamp(_laserCount.Value + 1, 0, _shipStatPreference.MaxLaserCount);
			}
			
			RecoveryLaserCountFlag = false;
		}
		
		private async void StartImmortalityFrame(float time)
		{
			_immortality.Value = true;
			_shipAnimationControl.SwitchBlinking(true);

			float timeCounter = time;

			while (time > 0.0f)
			{ 
				await Task.Delay(1000);
				time = Mathf.Clamp(time - 1.0f, 0.0f, float.MaxValue);
			}
			
			_immortality.Value = false;
			_shipAnimationControl.SwitchBlinking(false);
		}

		private void Death()
		{ 
			_lifeStatus.Value = false;
			_shipAnimationControl.Death();
		}
	}
}
