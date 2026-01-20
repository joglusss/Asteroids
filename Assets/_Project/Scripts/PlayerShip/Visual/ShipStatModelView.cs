using System;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using Zenject;

namespace Asteroids.Ship
{
	public class ShipStatViewModel : IInitializable, IDisposable
	{
		public ReadOnlyReactiveProperty<string> Health { get; private set; }
		public ReadOnlyReactiveProperty<string> Coordinates { get; private set; }
		public ReadOnlyReactiveProperty<string> Angle { get; private set; }
		public ReadOnlyReactiveProperty<string> Speed { get; private set; }
		public ReadOnlyReactiveProperty<string> LaserCount { get; private set; }
		public ReadOnlyReactiveProperty<string> LaserCooldown { get; private set; }
		public ReadOnlyReactiveProperty<bool> Immortality { get; private set; }
		public ReadOnlyReactiveProperty<bool> LifeStatus { get; private set; }



		private ShipStatModel _model;
		private CompositeDisposable _compositeDisposable = new CompositeDisposable();

		[Inject]
		private void Construct(ShipStatModel shipStatModel)
		{
			_model = shipStatModel;
			
			LifeStatus = _model.LifeStatus;
			Immortality = _model.Immortality;
		}

		public void Initialize()
		{
			Health = _model.Health.Select(x => $"Health: {x}").ToReadOnlyReactiveProperty();
			Coordinates = _model.Coordinates.Select(x => $"Coordinates: {x}").ToReadOnlyReactiveProperty();
			Angle = _model.Angle.Select(x => $"Angle: {x}").ToReadOnlyReactiveProperty();
			Speed = _model.Speed.Select(x => $"Speed: {x}").ToReadOnlyReactiveProperty();
			LaserCount = _model.LaserCount.Select(x => $"Laser Count: {x}").ToReadOnlyReactiveProperty();
			LaserCooldown = _model.LaserCooldown.Select(x => $"Laser Cooldown: {x}").ToReadOnlyReactiveProperty();
		}

		public void Resurrect() => _model.Resurrect().Forget();
		
		public void GiveUp() => _model.GiveUp().Forget();

		public bool IsImmortal() => _model.Immortality.Value;

		public bool IsAlive() => _model.LifeStatus.Value;

		public int GetLaserCount() => _model.LaserCount.Value;

		public void Dispose() => _compositeDisposable.Dispose();

		public void AddHealth(int a) => _model.AddHealth(a);

		public void SetCoordinates(Vector2 a) => _model.SetCoordinates(a);

		public void SetAngle(float a) => _model.SetAngle(a);

		public void SetSpeed(float a) => _model.SetSpeed(a);

		public void AddLaserCount(int a) => _model.AddLaserCount(a);
	}
}


