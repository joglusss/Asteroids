using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Asteroids.SceneManage;
using Asteroids.Ship;

namespace Asteroids.Input 
{
    public class InputStorage : MonoBehaviour, IInitialize
    {
        [SerializeField] private UnityEvent<Vector2> MoveEvent;
        [SerializeField] private UnityEvent LaserShotEvent;
        [SerializeField] private UnityEvent BulletShotEvent;
        [SerializeField] private UnityEvent EscapeEvent;

        private InputAction _move;
        private InputAction _laserShot;
        private InputAction _bulletShot;
        private InputAction _escape;

        private void Awake()
        {
            _move = InputSystem.actions.FindAction("Move");
            _bulletShot = InputSystem.actions.FindAction("Shot");
            _laserShot = InputSystem.actions.FindAction("Laser");
            _escape = InputSystem.actions.FindAction("Escape");

            _move.performed += MoveInvoke;
            _move.canceled += MoveInvoke;
            _laserShot.performed += LaserShotInvoke;
            _bulletShot.performed += BulletShotInvoke;
            _escape.performed += EscapeInvoke;
        }

        private void OnDestroy()
        {
            _move.performed -= MoveInvoke;
            _move.canceled -= MoveInvoke;
            _laserShot.performed -= LaserShotInvoke;
            _bulletShot.performed -= BulletShotInvoke;
            _escape.performed -= EscapeInvoke;

            MoveEvent.RemoveAllListeners();
            LaserShotEvent.RemoveAllListeners();
            BulletShotEvent.RemoveAllListeners();
            EscapeEvent.RemoveAllListeners();
        }


        public void Initialize(DependencyContainer dependencyContainer)
        {
            MoveEvent.AddListener(dependencyContainer.ShipLink.GetComponent<ShipControl>().SetInputValue);

            ShipWeapon shipWeaponTemp = dependencyContainer.ShipLink.GetComponent<ShipWeapon>();
            LaserShotEvent.AddListener(shipWeaponTemp.ShootLaser);
            BulletShotEvent.AddListener(shipWeaponTemp.ShootBullet);
        }

        private void MoveInvoke(InputAction.CallbackContext callbackContext) => MoveEvent?.Invoke(callbackContext.ReadValue<Vector2>());

        private void LaserShotInvoke(InputAction.CallbackContext callbackContext) => LaserShotEvent?.Invoke();

        private void BulletShotInvoke(InputAction.CallbackContext callbackContext) => BulletShotEvent?.Invoke();

        private void EscapeInvoke(InputAction.CallbackContext callbackContext) => EscapeEvent?.Invoke();
    }
}