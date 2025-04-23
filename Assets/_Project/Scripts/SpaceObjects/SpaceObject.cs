using UnityEngine;
using Asteroids.Visual;

namespace Asteroids.Objects
{
    public abstract class SpaceObject : MonoBehaviour
    {
        protected ObjectManager _objectManager { get; private set; }
        protected SpaceObjectQueue _spaceObjectQueue { get; private set; }

        private void OnDisable()
        {
            StopAllCoroutines();
            _spaceObjectQueue?.ReturnObject(this);
        }

        public virtual void Initialize(ObjectManager objectManager, SpaceObjectQueue spaceObjectQueue)
        {
            _objectManager = objectManager;
            _spaceObjectQueue = spaceObjectQueue;
        }

        public abstract void Launch(Vector2 from, Vector2 direction);
    }

    public enum SpaceObjectTypeEnum { SpaceShip, Asteroid, Alien }

    public interface ISpaceInteract
    {
        SpaceObjectTypeEnum SpaceObjectType { get;}
        void Interact(SpaceObjectTypeEnum collisionSpaceObjectType);
    }
}
