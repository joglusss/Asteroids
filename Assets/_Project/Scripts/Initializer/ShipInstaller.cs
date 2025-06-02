using Asteroids.Ship;
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

            Container.BindInterfacesAndSelfTo<ShipStatModel>().FromNew().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ShipStatPresenter>().FromNew().AsSingle().NonLazy();

            var shipInstance = Instantiate(_shipControl);

            Container.BindInterfacesAndSelfTo<ShipControl>().FromInstance(shipInstance.GetComponent<ShipControl>()).AsSingle().Lazy();
            Container.BindInterfacesAndSelfTo<ShipStat>().FromInstance(shipInstance.GetComponent<ShipStat>()).AsSingle().Lazy();
            Container.BindInterfacesAndSelfTo<ShipWeapon>().FromInstance(shipInstance.GetComponent<ShipWeapon>()).AsSingle().Lazy();

            Container.QueueForInject(shipInstance.GetComponent<ShipControl>());
            Container.QueueForInject(shipInstance.GetComponent<ShipStat>());
            Container.QueueForInject(shipInstance.GetComponent<ShipWeapon>());
        }
    }
}
