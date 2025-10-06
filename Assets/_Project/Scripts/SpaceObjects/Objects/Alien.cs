using Asteroids.Ship;
using UnityEngine;
using Zenject;

namespace Asteroids.Objects
{
    public class Alien : PhysicalSpaceObject
    {
        protected override float _speed => Config.AlienSpeed;
        
        private float _angularSpeed => Config.AlienAngularSpeed;

        [Inject] private ShipControl _target; 

        private void Update()
        {
            if (!IsPaused)
            {
                Vector2 force = _speed * Time.deltaTime * transform.up;
                float angle = Vector2.SignedAngle(transform.up, _target.transform.position - transform.position) * _angularSpeed * Time.deltaTime;

                _rigidbody.AddForce(force, ForceMode2D.Impulse);
                _rigidbody.AddTorque(angle);
            }
            else
            {
                _rigidbody.linearVelocity = Vector2.zero;
                _rigidbody.totalTorque = 0;
            }
        }
    }
}

