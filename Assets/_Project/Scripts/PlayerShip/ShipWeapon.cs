using System.Collections;
using UnityEngine;
using Asteroids.Visual;
using Asteroids.Objects;
using Asteroids.SceneManage;

namespace Asteroids.Ship
{
    public class ShipWeapon : MonoBehaviour, IInitialize
    {
        [SerializeField] private float _laserCooldownDelay;
        [SerializeField] private int _maxLaserShot;

        private ShipStatModel _model;
        private Coroutine _laserCounter;
        private ObjectManager _objectManager;

        public void Initialize(DependencyContainer dependencyContainer)
        {
            _objectManager = dependencyContainer.ObjectManagerLink;
            _model = dependencyContainer.ShipStatModelLink;

            _model.LaserCount = _maxLaserShot;
            _model.LaserCooldown = 0.0f;
        }

        private IEnumerator LaserCounter()
        {

            while (_model.LaserCount < _maxLaserShot)
            {
                _model.LaserCooldown = _laserCooldownDelay;
                while (_model.LaserCooldown > 0.0f)
                {
                    _model.LaserCooldown = Mathf.Clamp(_model.LaserCooldown - Time.deltaTime, 0.0f, _maxLaserShot);
                    _model.LaserCooldown = _model.LaserCooldown;
                    yield return null;
                }

                _model.LaserCount++;
            }

            _laserCounter = null;
        }

        public void ShootBullet() 
        {
            SpaceObject bullet = _objectManager.BulletQueue.DrawObject();
            if (bullet != null)
            {
                bullet.Launch(this.transform.position + this.transform.up, this.transform.up);
            }
        }

        public void ShootLaser() 
        {
            if (_model.LaserCount > 0)
            {
                SpaceObject laser = _objectManager.LaserQueue.DrawObject();
                if (laser != null)
                {
                    _model.LaserCount--;
                    laser.Launch(this.transform.position + this.transform.up, this.transform.up);
                }
            }

            if (_model.LaserCount < _maxLaserShot && _laserCounter == null)
                _laserCounter = StartCoroutine(LaserCounter());
        }
    }
}