using System.Collections;
using UnityEngine;
using Asteroids.Visual;
using Asteroids.Objects;
using Asteroids.SceneManage;
using Zenject;
using Asteroids.Input;

namespace Asteroids.Ship
{
    public class ShipWeapon : MonoBehaviour, IInitializable
    {
        [SerializeField] private float _laserCooldownDelay;
        [SerializeField] private int _maxLaserShot;

        private ShipStatModel _model;
        private IInput _inputStorage;
        private Coroutine _laserCounter;
        private ObjectManager _objectManager;

        private void OnDisable()
        {
            _inputStorage.LaserShotEvent -= ShootLaser;
            _inputStorage.BulletShotEvent -= ShootBullet;
        }

        public void Initialize()
        {
            _model.LaserCount = _maxLaserShot;
            _model.LaserCooldown = 0.0f;

            _inputStorage.LaserShotEvent += ShootLaser;
            _inputStorage.BulletShotEvent += ShootBullet;
        }

        [Inject]
        private void Construct(ObjectManager objectManager, ShipStatModel shipStatModel, IInput inputStorage)
        {
            _objectManager = objectManager;
            _model = shipStatModel;
            _inputStorage = inputStorage;
        }

        private IEnumerator LaserCounter()
        {

            while (_model.LaserCount < _maxLaserShot)
            {
                _model.LaserCooldown = _laserCooldownDelay;
                while (_model.LaserCooldown > 0.0f)
                {
                    _model.LaserCooldown = (Mathf.Clamp(_model.LaserCooldown - Time.deltaTime, 0.0f, _maxLaserShot));
                    yield return null;
                }

                _model.LaserCount++;
            }

            _laserCounter = null;
        }

        private void ShootBullet() 
        {
            if (!_model.LifeStatus) return;

            SpaceObject bullet = _objectManager.BulletQueue.DrawObject();
            if (bullet != null)
            {
                bullet.Launch(transform.position + transform.up, transform.up);
            }
        }

        private void ShootLaser() 
        {
            if (!_model.LifeStatus) return;

            if (_model.LaserCount > 0)
            {
                SpaceObject laser = _objectManager.LaserQueue.DrawObject();
                if (laser != null)
                {
                    _model.LaserCount--;
                    laser.Launch(transform.position + transform.up, transform.up);
                }
            }

            if (_model.LaserCount < _maxLaserShot && _laserCounter == null)
                _laserCounter = StartCoroutine(LaserCounter());
        }
    }
}