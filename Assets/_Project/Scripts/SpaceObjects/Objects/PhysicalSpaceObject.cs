using Asteroids.Total;
using UnityEngine;

namespace Asteroids.Objects 
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class PhysicalSpaceObject : SpaceObject, ISpaceInteract
    {
        protected virtual float Speed => Config.AsteroidsSpeed;
        protected Rigidbody2D _rigidbody;

        private Vector3 _saveVelocity;

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
            _rigidbody.AddForce(direction.normalized * Speed, ForceMode2D.Impulse);
        }

        protected override void OnPause(bool value)
        {
            if (!value)
            {
                _saveVelocity = _rigidbody.linearVelocity;
                _rigidbody.Sleep();
            }
            else
            { 
                _rigidbody.WakeUp();
                _rigidbody.linearVelocity = _saveVelocity;
            }
        }

        public void Interact(SpaceObjectType collisionSpaceObjectType)
        {
            if (SpaceObjectType != collisionSpaceObjectType)
                Demolish();
        }

        protected virtual void Demolish()
        {
            ReturnToQueue();
        }
    }

}

