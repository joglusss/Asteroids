using Asteroids.Objects;
using UnityEngine;
using Zenject;

namespace Asteroids.Installers
{ 
    public class LaunchCycleManagerInstaller : MonoInstaller
    {
        [SerializeField] private LaunchCycleManager _objectManager;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<LaunchCycleManager>().FromInstance(_objectManager).AsSingle();
        }
        
    }
}


