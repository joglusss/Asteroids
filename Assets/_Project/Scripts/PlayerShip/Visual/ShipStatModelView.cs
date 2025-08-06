using System;
using R3;
using UnityEngine;
using Zenject;

namespace Asteroids.Ship
{ 
	public class ShipStatViewModel : IInitializable, IDisposable
	{
		public ReadOnlyReactiveProperty<int> Health { get; private set; }
		public ReadOnlyReactiveProperty<bool> Immortality { get; private set; }
		public ReadOnlyReactiveProperty<Vector2> Coordinates { get; private set; }
		public ReadOnlyReactiveProperty<float> Angle { get; private set; }
		public ReadOnlyReactiveProperty<float> Speed { get; private set; }
		public ReadOnlyReactiveProperty<int> LaserCount { get; private set; }
		public ReadOnlyReactiveProperty<float> LaserCooldown { get; private set; }
		public ReadOnlyReactiveProperty<bool> LifeStatus { get; private set; }
		
		private ShipStatModel _model;
		private CompositeDisposable _compositeDisposable = new CompositeDisposable();
		
		[Inject]
		private void Construct(ShipStatModel shipStatModel)
		{
			_model = shipStatModel;
		}

		public void Initialize()
		{
			Health = _model._health;
			Immortality = _model._immortality;
			Coordinates = _model._coordinates;
			Angle = _model._angle;
			Speed = _model._speed;
			LaserCount = _model._laserCount;
			LaserCooldown = _model._laserCooldown;
			LifeStatus = _model._lifeStatus;
		}

		public void Dispose() => _compositeDisposable.Dispose();

		public void AddHealth(int a) => _model.AddHealth(a);

		public void SetCoordinates(Vector2 a) => _model.SetCoordinates(a);

		public void SetAngle(float a) => _model.SetAngle(a);

		public void SetSpeed(float a) => _model.SetSpeed(a);

		public void AddLaserCount(int a) => _model.AddLaserCount(a);
	}
}


