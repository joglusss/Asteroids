using System;
using UnityEngine;

namespace Asteroids.Objects
{
    public abstract class SpaceObject : MonoBehaviour
    {
        protected ObjectManager ObjectManager { get; private set; }

        private Action _returningDelegate;

        protected void OnDisable()
        {
            StopAllCoroutines();
        }

        protected void ReturnToQueue() => _returningDelegate.Invoke();

        public void Initialize(ObjectManager objectManager, Action ReturningDelegate)
        {
            ObjectManager = objectManager;
            _returningDelegate = ReturningDelegate;
        }

        public abstract void Launch(Vector2 from, Vector2 direction);
    }
}
