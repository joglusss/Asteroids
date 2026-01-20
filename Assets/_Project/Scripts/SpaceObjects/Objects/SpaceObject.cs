using Asteroids.Ship;
using Asteroids.Total;
using R3;
using UnityEngine;
using Zenject;
using Asteroids.Effect;

namespace Asteroids.Objects
{
    [RequireComponent(typeof(ZenAutoInjecter))]
    public abstract class SpaceObject : MonoBehaviour
    {
        public Subject<SpaceObject> OnLifeEnd = new();
        public ReactiveCommand OnLaunch = new();
        public ReactiveCommand OnDestroy = new();
        protected LaunchCycleManager ObjectManager { get; private set; }
        protected Config Config { get; private set; }
        protected bool IsPaused { get; private set; }


        private ShipStatViewModel _shipStatVM;

        protected void OnDisable()
        {
            StopAllCoroutines();
        }

        [Inject]
        public void Construct(LaunchCycleManager objectManager, ShipStatViewModel shipStatVM, SaveService saveService, EffectPlayer effectPlayer)
        { 
            ObjectManager = objectManager;
            Config = saveService.DataState.Config;
            _shipStatVM = shipStatVM;
        }
        
        public void Initialize()
        {
            _shipStatVM.LifeStatus.Skip(1).Subscribe(OnPause).AddTo(this);
            _shipStatVM.LifeStatus.Subscribe(value => IsPaused = !value).AddTo(this);
        }

        protected void ReturnToQueue() => OnLifeEnd.OnNext(this);

        public abstract void Launch(Vector2 from, Vector2 direction);

        protected abstract void OnPause(bool value);
    }
}
