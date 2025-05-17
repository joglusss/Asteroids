using UnityEngine;

namespace Asteroids.Objects
{
    public class Alien : PhysicalSpaceObject
    {
        [SerializeField] private float _angularSpeed;
        
        private Transform _target;

        public override void Initialize(ObjectManager objectManager, SpaceObjectQueue spaceObjectQueue)
        {
            base.Initialize(objectManager, spaceObjectQueue);

            _target = objectManager.AlienTarget;
        }

        private void Update()
        {
            Vector2 force = _speed * Time.deltaTime * transform.up;
            float angle = Vector2.SignedAngle(transform.up, _target.position - transform.position) * _angularSpeed * Time.deltaTime;

            _rigidbody.AddForce(force, ForceMode2D.Impulse);
            _rigidbody.AddTorque(angle);
        }
    }
}

