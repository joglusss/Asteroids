using UnityEngine;
using System;
using System.Collections;
using Asteroids.Visual;

namespace Asteroids.Objects
{
    public abstract class SpaceObject : MonoBehaviour, IResetable
    {
        [field: SerializeField] protected ObjectManager m_ObjectManager { get; private set; }

        [field: SerializeField] public float Lifetime { get; set; }
        protected Coroutine coroutineLifetimeCounter;
        protected IEnumerator LifetimeCounter()
        {
            if (Lifetime < 0.0f || float.IsInfinity(Lifetime))
                yield break;

            yield return new WaitForSeconds(Lifetime);

            coroutineLifetimeCounter = null;
            ReturnDelegate.Invoke();
        }

        public abstract void Launch(Vector2 from, Vector2 direction);
        public Action ReturnDelegate;

        public void StopGame()
        {
            ReturnDelegate.Invoke();
        }

        public void StartGame() { }

        protected virtual void Awake()
        {
            ((IResetable)this).InitialazeIRessetable();
        }
        public abstract void OnStartGame();
    }

    public interface ISpaceInteract
    {

        public enum SpaceObjectTypeEnum { SpaceShip, Asteroid, Alien }
        public SpaceObjectTypeEnum SpaceObjectType { get; set; }

        public void Interact(SpaceObjectTypeEnum collisionSpaceObjectType);
    }

}
