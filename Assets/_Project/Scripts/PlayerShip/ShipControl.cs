using UnityEngine;
using Asteroids.Helpers;
using Asteroids.Visual;
using Asteroids.SceneManage;
using Zenject;
using Asteroids.Input;

namespace Asteroids.Ship
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class ShipControl : MonoBehaviour, IInitializable
    {
        [SerializeField] private float _forwardSpeed = 7.0f;
        [SerializeField] private float _angularSpeed = -1000.0f;

        private ShipStatModel _model;
        private InputStorage _inputStorage;
        private Rigidbody2D _rigidbody;
        private Vector2 _inputValue;
        private Vector2[] _borderPoints;
        private Vector2 _centerPoint;

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

            _rigidbody = GetComponent<Rigidbody2D>();
            _rigidbody.position = _centerPoint;

            _inputStorage.MoveEvent += SetInputValue;
        }

        [Inject]
        private void Construct(ShipStatModel statModel, InputStorage inputStorage)
        {
            _model = statModel;
            _inputStorage = inputStorage;
        }

        private void SetInputValue(Vector2 vector) => _inputValue = vector;

        private void Movement()
        {
            Vector2 force = _inputValue.y > 0.0f ? _inputValue.y * _forwardSpeed * Time.deltaTime * transform.up : Vector2.zero;
            float angle = _inputValue.x * _angularSpeed * Time.deltaTime;

            _rigidbody.AddForce(force, ForceMode2D.Impulse);
            _rigidbody.AddTorque(angle);
        }

        private void UpdateModel()
        {
            _model.Coordinate = _rigidbody.position;
            _model.Speed = _rigidbody.linearVelocity.magnitude;
            _model.Angle = transform.eulerAngles.z;
        }
    }

}
