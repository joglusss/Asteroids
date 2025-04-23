using System.Collections;
using UnityEngine;
using Asteroids.Objects;
using UnityEngine.InputSystem;

namespace Asteroids.Ship
{
    [RequireComponent(typeof(Collider2D))]
    public class ShipWeapon : MonoBehaviour
    {
        [SerializeField] private ObjectManager _objectManager;
        [SerializeField] private float _laserCooldown;
        [SerializeField] private int _maxLaserShot;

        private Coroutine _laserCounter;

        public float CurrentLaserCooldown { get; private set; }
        public int CurrentAvailableLaser { get; private set; }

        private void OnEnable()
        {
            CurrentAvailableLaser = _maxLaserShot;

            InputSystem.actions.FindAction("Shot").performed += ShootBullet;
            InputSystem.actions.FindAction("Laser").performed += ShootLaser;
        }

        private void OnDisable()
        {
            InputSystem.actions.FindAction("Shot").performed -= ShootBullet;
            InputSystem.actions.FindAction("Laser").performed -= ShootLaser;
        }

        private IEnumerator LaserCounter()
        {

            while (CurrentAvailableLaser < _maxLaserShot)
            {
                float time = _laserCooldown;
                while (time > 0.0f)
                {
                    time = Mathf.Clamp(time - Time.deltaTime, 0.0f, _maxLaserShot);
                    CurrentLaserCooldown = time;
                    yield return null;
                }

                CurrentAvailableLaser++;
            }

            _laserCounter = null;
        }

        private void ShootBullet(InputAction.CallbackContext callback) {

            SpaceObject bullet = _objectManager.BulletQueue.DrawObject();
            if (bullet != null)
            {
                bullet.Launch(this.transform.position + this.transform.up, this.transform.up);
            }
        }

        private void ShootLaser(InputAction.CallbackContext callback) {
            if (CurrentAvailableLaser > 0)
            {
                SpaceObject laser = _objectManager.LaserQueue.DrawObject();
                if (laser != null)
                {
                    CurrentAvailableLaser--;
                    laser.Launch(this.transform.position + this.transform.up, this.transform.up);
                }
            }

            if (CurrentAvailableLaser < _maxLaserShot && _laserCounter == null)
                _laserCounter = StartCoroutine(LaserCounter());
        }
    }
}