using System.Collections;
using UnityEngine;
using Asteroids.Visual;
using Asteroids.Objects;
using Asteroids.SceneManage;
using Zenject;
using Asteroids.Input;
using Asteroids.Asset;

namespace Asteroids.Ship
{
	public class ShipWeapon : MonoBehaviour, IInitializable
	{
		[SerializeField] private float _laserCooldownDelay;
		[SerializeField] private int _maxLaserShot;

		private ShipStatViewModel _viewModel;
		private IInput _inputStorage;
		private ObjectManager _objectManager;

		[Inject]
		private void Construct(ObjectManager objectManager, ShipStatViewModel viewModel, IInput inputStorage)
		{
			_objectManager = objectManager;
			_viewModel = viewModel;
			_inputStorage = inputStorage;
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

			SpaceObject bullet = _objectManager.BulletQueue.DrawObject();
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
				SpaceObject laser = _objectManager.LaserQueue.DrawObject();
				if (laser != null)
				{
					_viewModel.AddLaserCount(-1);
					laser.Launch(transform.position + transform.up, transform.up);
				}
			}
		}
	}
}