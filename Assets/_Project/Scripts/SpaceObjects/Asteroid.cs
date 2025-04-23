using UnityEngine;
using Asteroids.Helpers;

namespace Asteroids.Objects
{
    public class Asteroid : PhysicalSpaceObject
    {
        [SerializeField] bool _isSpliting;

        private void Update()
        {
            if (GameMath.IsOutCanvas(m_rigidbody.position, out Vector2 wallSide, _objectManager.CanvasSize))
            {
                GameMath.BorderTeleport(m_rigidbody, wallSide, _objectManager.CanvasSize);
            }
        }

        protected override void Demolish()
        {
            if (_isSpliting)
                for (int i = 0; i < 3; i++)
                    _objectManager.SmallAsteroidQueue.DrawObject().Launch(m_rigidbody.position, new Vector2(UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(-1.0f, 1.0f)));

            base.Demolish();
        }
    }

}
