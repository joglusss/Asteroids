using Asteroids.Ship;
using UnityEngine;
using Zenject;
using R3;

namespace Asteroids.Objects
{
    public class Alien : PhysicalSpaceObject
    {
        protected override float Speed => Config.AlienSpeed;
        
        private float AngularSpeed => Config.AlienAngularSpeed;

        [Inject] private ShipControl _target; 

        private void Update()
        {
            if (!IsPaused)
            {
                Vector2 force = Speed * Time.deltaTime * transform.up;
                float angle = Vector2.SignedAngle(transform.up, _target.transform.position - transform.position) * AngularSpeed * Time.deltaTime;

                _rigidbody.AddForce(force, ForceMode2D.Impulse);
                _rigidbody.AddTorque(angle);
            }
            else
            {
                _rigidbody.linearVelocity = Vector2.zero;
                _rigidbody.totalTorque = 0;
            }
        }

        protected override void Demolish()
        {
            OnDestroy.Execute(Unit.Default);

            base.Demolish();
        }
    }
}

