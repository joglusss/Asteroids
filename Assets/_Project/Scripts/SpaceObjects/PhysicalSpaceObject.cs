using System.Collections;
using UnityEngine;

namespace Asteroids.Objects 
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class PhysicalSpaceObject : SpaceObject, ISpaceInteract
    {
        [SerializeField] protected float _speed;

        protected Rigidbody2D m_rigidbody;

        [field: SerializeField] public SpaceObjectTypeEnum SpaceObjectType { get; private set; }

        protected virtual void Awake()
        {
            m_rigidbody = GetComponent<Rigidbody2D>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out ISpaceInteract spaceObject))
                Interact(spaceObject.SpaceObjectType);
        }

        public override void Launch(Vector2 from, Vector2 direction)
        {
            m_rigidbody.linearVelocity = Vector2.zero;
            m_rigidbody.transform.position = from;
            m_rigidbody.AddForce(direction.normalized * _speed, ForceMode2D.Impulse);
        }

        public void Interact(SpaceObjectTypeEnum collisionSpaceObjectType)
        {
            if (SpaceObjectType != collisionSpaceObjectType)
                Demolish();
        }

        protected virtual void Demolish()
        {
            _spaceObjectQueue.ReturnObject(this);
        }
    }

}

