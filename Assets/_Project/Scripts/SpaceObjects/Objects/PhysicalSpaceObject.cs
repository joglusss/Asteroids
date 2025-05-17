using UnityEngine;

namespace Asteroids.Objects 
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class PhysicalSpaceObject : SpaceObject, ISpaceInteract
    {
        [SerializeField] protected float _speed;

        protected Rigidbody2D _rigidbody;

        [field: SerializeField] public SpaceObjectType SpaceObjectType { get; private set; }

        protected virtual void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out ISpaceInteract spaceObject))
                Interact(spaceObject.SpaceObjectType);
        }

        public override void Launch(Vector2 from, Vector2 direction)
        {
            _rigidbody.linearVelocity = Vector2.zero;
            _rigidbody.transform.position = from;
            _rigidbody.AddForce(direction.normalized * _speed, ForceMode2D.Impulse);
        }

        public void Interact(SpaceObjectType collisionSpaceObjectType)
        {
            if (SpaceObjectType != collisionSpaceObjectType)
                Demolish();
        }

        protected virtual void Demolish()
        {
            SpaceObjectQueue.ReturnObject(this);
        }
    }

}

