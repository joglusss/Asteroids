using System;
using Asteroids.Ship;
using Asteroids.Total;
using Mono.Cecil;
using R3;
using UnityEngine;
using Zenject;

namespace Asteroids.Objects
{
    [RequireComponent(typeof(ZenAutoInjecter))]
    public abstract class SpaceObject : MonoBehaviour
    {
        protected LaunchCycleManager ObjectManager { get; private set; }
        protected Config Config { get; private set; }
        protected bool IsPaused { get; private set; }

        private Action _returningDelegate;
        private ShipStatViewModel _shipStatVM;

        protected void OnDisable()
        {
            StopAllCoroutines();
        }

        [Inject]
        public void Construct(LaunchCycleManager objectManager, ShipStatViewModel shipStatVM, SaveService saveService)
        { 
            ObjectManager = objectManager;
            Config = saveService.Data.Config;
            _shipStatVM = shipStatVM;
        }
        
        public void Initialize(Action ReturningDelegate)
        {
            _returningDelegate = ReturningDelegate;
            _shipStatVM.LifeStatus.Skip(1).Subscribe(OnPause).AddTo(this);
            _shipStatVM.LifeStatus.Subscribe(value => IsPaused = !value).AddTo(this);
        }

        protected void ReturnToQueue() => _returningDelegate.Invoke();

        public abstract void Launch(Vector2 from, Vector2 direction);

        protected abstract void OnPause(bool value);
    }
}
