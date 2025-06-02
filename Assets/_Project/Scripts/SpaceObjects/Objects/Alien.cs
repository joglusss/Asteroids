using UnityEngine;

namespace Asteroids.Objects
{
    public class Alien : PhysicalSpaceObject
    {
        [SerializeField] private float _angularSpeed;

        private Transform Target => ObjectManager.AlienTarget;

        private void Update()
        {
            Vector2 force = _speed * Time.deltaTime * transform.up;
            float angle = Vector2.SignedAngle(transform.up, Target.position - transform.position) * _angularSpeed * Time.deltaTime;

            _rigidbody.AddForce(force, ForceMode2D.Impulse);
            _rigidbody.AddTorque(angle);
        }
    }
}

