using UnityEngine;
using UnityEngine.InputSystem;
using Asteroids.Helpers;

namespace Asteroids.Objects
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class ShipControl : MonoBehaviour
    {
        [SerializeField] private ObjectManager _objectManager;
        [SerializeField] private float _forwardSpeed = 7.0f;
        [SerializeField] private float _angularSpeed = -1000.0f;

        private Rigidbody2D _rigidbody;
        private InputAction _moveInputAction;

        public Vector2 Position { get => _rigidbody.position; }
        public Vector2 Velosity { get => _rigidbody.linearVelocity; }
        public float Angle { get => _rigidbody.rotation; }

        private void Update()
        {
            Movement();

            if (GameMath.IsOutCanvas(_rigidbody.position, out Vector2 wallSide, _objectManager.CanvasSize))
            {
                GameMath.BorderTeleport(_rigidbody, wallSide, _objectManager.CanvasSize);
            }
        }

        private void OnEnable()
        {
            _rigidbody.position = _objectManager.CanvasSize / 2.0f;
        }

        public void Init()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _moveInputAction = InputSystem.actions.FindAction("Move");
        }

        private void Movement()
        {
            Vector2 inputValue = _moveInputAction.ReadValue<Vector2>().normalized;

            Vector2 force = inputValue.y > 0.0f ? (Vector2)transform.up * inputValue.y * _forwardSpeed * Time.deltaTime : Vector2.zero;
            float angle = inputValue.x * _angularSpeed * Time.deltaTime;

            _rigidbody.AddForce(force, ForceMode2D.Impulse);
            _rigidbody.AddTorque(angle);
        }        
    }

}
