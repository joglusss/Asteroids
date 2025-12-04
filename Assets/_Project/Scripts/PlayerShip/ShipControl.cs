using UnityEngine;
using Asteroids.Helpers;
using Zenject;
using Asteroids.Input;
using System;
using Asteroids.Total;

namespace Asteroids.Ship
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class ShipControl : MonoBehaviour, IInitializable, IDisposable
    {
        private Config _config;
        private ShipStatViewModel _viewModel;
        private BorderSetting _borderSetting;
        private IInput _inputStorage;
        private Rigidbody2D _rigidbody;
        private Vector2 _inputValue;


        [Inject]
        private void Construct(SaveService saveService, BorderSetting borderSetting, ShipStatViewModel viewModel, IInput inputStorage)
        {
            _config = saveService.DataState.Config;
            _borderSetting = borderSetting;
            _viewModel = viewModel;
            _inputStorage = inputStorage;
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            Movement();
            UpdateModel();

            GameMath.TeleportToBorder(_rigidbody, _borderSetting);
        }

        private void OnDisable()
        {
            _inputStorage.MoveEvent -= SetInputValue;
        }

        public void Initialize()
        {
            _rigidbody.position = _borderSetting.Center;
            _inputStorage.MoveEvent += SetInputValue;
        }

        public void Dispose()
        {
            _inputStorage.MoveEvent -= SetInputValue;
        }

        private void SetInputValue(Vector2 vector) => _inputValue = vector;

        private void Movement()
        {
            if (!_viewModel.IsAlive()) return;

            Vector2 force = _inputValue.y > 0.0f ? _inputValue.y * _config.SpaceShipForwardSpeed * Time.deltaTime * transform.up : Vector2.zero;
            float angle = _inputValue.x * _config.SpaceShipAngularSpeed * Time.deltaTime;

            _rigidbody.AddForce(force, ForceMode2D.Impulse);
            _rigidbody.AddTorque(angle);
        }

        private void UpdateModel()
        {
            _viewModel.SetCoordinates(_rigidbody.position);
            _viewModel.SetSpeed(_rigidbody.linearVelocity.magnitude);
            _viewModel.SetAngle(transform.eulerAngles.z);
        }
    }
}
