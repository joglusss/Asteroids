using UnityEngine;
using Asteroids.Helpers;
using Zenject;
using Asteroids.Input;
using System;

namespace Asteroids.Ship
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class ShipControl : MonoBehaviour, IInitializable, IDisposable
    {
        [SerializeField] private float _forwardSpeed = 7.0f;
        [SerializeField] private float _angularSpeed = -1000.0f;

        private ShipStatViewModel _viewModel;
        private IInput _inputStorage;
        private Rigidbody2D _rigidbody;
        private Vector2 _inputValue;
        private Vector2[] _borderPoints;
        private Vector2 _centerPoint;

        [Inject]
        private void Construct(ShipStatViewModel viewModel, IInput inputStorage)
        {
            _viewModel = viewModel;
            _inputStorage = inputStorage;
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            Movement();
            UpdateModel();

            GameMath.TeleportToBorder(_rigidbody, _centerPoint, _borderPoints);
        }

        private void OnDisable()
        {
            _inputStorage.MoveEvent -= SetInputValue;
        }

        public void Initialize()
        {
            _borderPoints = GameMath.CalculateBorderPoints();
            _centerPoint = GameMath.BorderCenter();

            _rigidbody.position = _centerPoint;

            _inputStorage.MoveEvent += SetInputValue;
        }

        public void Dispose()
        {
            _inputStorage.MoveEvent -= SetInputValue;
        }

        private void SetInputValue(Vector2 vector) => _inputValue = vector;

        private void Movement()
        {
            if (!_viewModel.LifeStatus.CurrentValue) return;

            Vector2 force = _inputValue.y > 0.0f ? _inputValue.y * _forwardSpeed * Time.deltaTime * transform.up : Vector2.zero;
            float angle = _inputValue.x * _angularSpeed * Time.deltaTime;

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
