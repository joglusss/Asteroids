using System.Collections;
using UnityEngine;

namespace Asteroids.Objects
{
    public class Bullet : PhysicalSpaceObject
    {
        protected override float _speed => Config.BulletSpeed;
    
        [SerializeField] private float _lifeTime;

        public override void Launch(Vector2 from, Vector2 direction)
        {
            base.Launch(from, direction);
            StartCoroutine(LifetimeCounter());
        }

        private IEnumerator LifetimeCounter()
        {
            yield return new WaitForSeconds(_lifeTime);
            ReturnToQueue();
        }
    }
}
