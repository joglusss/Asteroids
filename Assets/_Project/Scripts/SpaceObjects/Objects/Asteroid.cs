using UnityEngine;
using Asteroids.Helpers;

namespace Asteroids.Objects
{
    public class Asteroid : PhysicalSpaceObject
    {
        [SerializeField] bool _isSeparable;

        private void Update()
        {
                GameMath.TeleportToBorder(_rigidbody, ObjectManager.BorderCenter, ObjectManager.BorderPoints);
        }

        protected override void Demolish()
        {
            if (_isSeparable)
                for (int i = 0; i < 3; i++)
                    ObjectManager.SmallAsteroidQueue.DrawObject().Launch(_rigidbody.position, new Vector2(UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(-1.0f, 1.0f)));

            base.Demolish();
        }
    }

}
