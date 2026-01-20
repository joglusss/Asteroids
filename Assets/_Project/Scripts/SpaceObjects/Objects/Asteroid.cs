using UnityEngine;
using Asteroids.Helpers;
using Zenject;
using Asteroids.Total;
using R3;

namespace Asteroids.Objects
{
    public class Asteroid : PhysicalSpaceObject
    {
        protected override float Speed => Config.AsteroidsSpeed;
    
        [SerializeField] private bool _isSeparable;

        [Inject(Id = SpaceObjectID.SmallAsteroid)] private SpaceObjectQueue _smallQueue;
        [Inject] private BorderSetting _borderSetting;

        private void Update()
        {
            GameMath.TeleportToBorder(_rigidbody, _borderSetting);
        }

        protected override void Demolish()
        {
            if (_isSeparable)
                for (int i = 0; i < 3; i++)
                    _smallQueue.DrawObject().Launch(_rigidbody.position, new Vector2(UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(-1.0f, 1.0f)));

            OnDestroy.Execute(Unit.Default);

            base.Demolish();
        }
    }

}
