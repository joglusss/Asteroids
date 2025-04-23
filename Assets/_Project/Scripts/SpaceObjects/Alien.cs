
using UnityEngine;

namespace Asteroids.Objects
{
    public class Alien : PhysicalSpaceObject
    {
        [SerializeField] public float _angularSpeed;
        
        private Transform _target;

        public override void Initialize(ObjectManager objectManager, SpaceObjectQueue spaceObjectQueue)
        {
            base.Initialize(objectManager, spaceObjectQueue);

            _target = objectManager.PlayerShip;
        }

        private void Update()
        {
            Vector2 force = (Vector2)transform.up * _speed * Time.deltaTime;
            float angle = Vector2.SignedAngle(transform.up, _target.position - transform.position) * _angularSpeed * Time.deltaTime;

            m_rigidbody.AddForce(force, ForceMode2D.Impulse);
            m_rigidbody.AddTorque(angle);
        }
    }
}

