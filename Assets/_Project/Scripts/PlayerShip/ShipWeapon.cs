using UnityEngine;
using Asteroids.Objects;
using Zenject;
using Asteroids.Input;

namespace Asteroids.Ship
{
	public class ShipWeapon : MonoBehaviour, IInitializable
	{
		private ShipStatViewModel _viewModel;
		private IInput _inputStorage;
		private SpaceObjectQueue _bullet;
		private SpaceObjectQueue _laser;

		[Inject]
		private void Construct(
			ShipStatViewModel viewModel, 
			IInput inputStorage,
			[Inject(Id = SpaceObjectID.Bullet)] SpaceObjectQueue bullet,
            [Inject(Id = SpaceObjectID.Laser)] SpaceObjectQueue laser
			)
		{
			_viewModel = viewModel;
			_inputStorage = inputStorage;
			_bullet = bullet;
			_laser = laser;
		}
		
		private void OnDisable()
		{
			_inputStorage.LaserShotEvent -= ShootLaser;
			_inputStorage.BulletShotEvent -= ShootBullet;
		}

		public void Initialize()
		{

			_inputStorage.LaserShotEvent += ShootLaser;
			_inputStorage.BulletShotEvent += ShootBullet;
		}
		
		private void ShootBullet() 
		{
			if (!_viewModel.IsAlive()) return;

			SpaceObject bullet = _bullet.DrawObject();
			if (bullet != null)
			{
				bullet.Launch(transform.position + transform.up, transform.up);
			}
		}

		private void ShootLaser() 
		{
			if (!_viewModel.IsAlive()) return;

			if (_viewModel.GetLaserCount() > 0)
			{
				SpaceObject laser = _laser.DrawObject();
				if (laser != null)
				{
					_viewModel.AddLaserCount(-1);
					laser.Launch(transform.position + transform.up, transform.up);
				}
			}
		}
	}
}