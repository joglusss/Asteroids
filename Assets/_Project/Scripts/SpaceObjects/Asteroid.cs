using UnityEngine;

namespace Asteroids.Objects
{
    public class Asteroid : Bullet
    {

        [field: SerializeField] bool IsSpliting { get; set; }

        public override void Demolish()
        {
            if (coroutineLifetimeCounter != null)
                StopCoroutine(coroutineLifetimeCounter);

            if (IsSpliting)
                for (int i = 0; i < 3; i++)
                    m_ObjectManager.smallAsteroid.DrawObject().Launch(m_rigidbody.position, new Vector2(UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(-1.0f, 1.0f)));


            ReturnDelegate.Invoke();
        }

        private void Update()
        {
            if (m_ObjectManager.IsOutCanvas(m_rigidbody.position, out Vector2 wallSide))
            {
                m_ObjectManager.BorderTeleport(m_rigidbody, wallSide);
            }
        }
    }

}
