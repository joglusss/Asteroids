using Asteroids.Ship;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Asteroids.Installers
{
    public class ShipInstaller : MonoInstaller
    {
        [SerializeField] private ShipControl _shipControl;
        [SerializeField] private ShipStatView _shipStatView;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ShipStatView>().FromInstance(_shipStatView).AsSingle();

            Container.BindInterfacesAndSelfTo<ShipStatModel>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ShipStatPresenter>().AsSingle().NonLazy();

            System.Type[] types = new System.Type[] 
            {   
                typeof(ShipAnimationControl),
                typeof(ShipControl), 
                typeof(ShipStat), 
                typeof(ShipWeapon),
                typeof(IInitializable), 
                typeof(ILateDisposable)
            };
            Container.Bind(types).FromComponentsInNewPrefab(_shipControl).AsSingle().Lazy();
        }
    }
}
