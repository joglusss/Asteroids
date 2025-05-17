using Unity.VisualScripting;
using UnityEngine;

namespace Asteroids.Objects
{
    public abstract class SpaceObject : MonoBehaviour
    {
        protected ObjectManager ObjectManager { get; private set; }
        protected SpaceObjectQueue SpaceObjectQueue { get; private set; }

        protected void OnDisable()
        {
            StopAllCoroutines();
        }

        public virtual void Initialize(ObjectManager objectManager, SpaceObjectQueue spaceObjectQueue)
        {
            ObjectManager = objectManager;
            SpaceObjectQueue = spaceObjectQueue;
        }

        public abstract void Launch(Vector2 from, Vector2 direction);
    }
}
