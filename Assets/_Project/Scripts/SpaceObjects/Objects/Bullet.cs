using System.Collections;
using UnityEngine;
using R3;

namespace Asteroids.Objects
{
    public class Bullet : PhysicalSpaceObject
    {
        protected override float Speed => Config.BulletSpeed;
    
        [SerializeField] private float _lifeTime;

        public override void Launch(Vector2 from, Vector2 direction)
        {
            base.Launch(from, direction);
            OnLaunch.Execute(Unit.Default);
            StartCoroutine(LifetimeCounter());
        }

        private IEnumerator LifetimeCounter()
        {
            yield return new WaitForSeconds(_lifeTime);
            ReturnToQueue();
        }
        protected override void Demolish()
        {
            OnDestroy.Execute(Unit.Default);
            base.Demolish();
        }
    }
}
