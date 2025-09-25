using System;
using Asteroids.Ship;
using Mono.Cecil;
using R3;
using UnityEngine;
using Zenject;

namespace Asteroids.Objects
{
    [RequireComponent(typeof(ZenAutoInjecter))]
    public abstract class SpaceObject : MonoBehaviour
    {
        protected ObjectManager ObjectManager { get; private set; }

        private Action _returningDelegate;
        private ShipStatViewModel _shipStatVM;

        protected void OnDisable()
        {
            StopAllCoroutines();
        }

        [Inject]
        public void Construct(ObjectManager objectManager, ShipStatViewModel shipStatVM)
        { 
            ObjectManager = objectManager;
            
            _shipStatVM = shipStatVM;
        }
        
        public void Initialize(Action ReturningDelegate)
        {
            _returningDelegate = ReturningDelegate;
            
            _shipStatVM.LifeStatus.Skip(1).Subscribe(Pause).AddTo(this);
        }

        protected void ReturnToQueue() => _returningDelegate.Invoke();

        public abstract void Launch(Vector2 from, Vector2 direction);
        
        protected abstract void Pause(bool value);
    }
}
