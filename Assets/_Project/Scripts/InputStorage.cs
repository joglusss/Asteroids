using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Zenject;

namespace Asteroids.Input 
{
    public class InputStorage : IInitializable
    {
        public Action<Vector2> MoveEvent;
        public Action LaserShotEvent;
        public Action BulletShotEvent;
        public Action EscapeEvent;

        private InputAction _move;
        private InputAction _laserShot;
        private InputAction _bulletShot;
        private InputAction _escape;

        public void Initialize()
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

        private void MoveInvoke(InputAction.CallbackContext callbackContext) => MoveEvent?.Invoke(callbackContext.ReadValue<Vector2>());

        private void LaserShotInvoke(InputAction.CallbackContext callbackContext) => LaserShotEvent?.Invoke();

        private void BulletShotInvoke(InputAction.CallbackContext callbackContext) => BulletShotEvent?.Invoke();

        private void EscapeInvoke(InputAction.CallbackContext callbackContext) => EscapeEvent?.Invoke();
    }
}