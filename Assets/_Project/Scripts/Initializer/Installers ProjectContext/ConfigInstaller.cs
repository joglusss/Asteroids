
using UnityEngine;
using Zenject;

namespace Asteroids.Total.Installers
{ 
    public class ConfigInstaller : MonoInstaller
    {
        public override async void InstallBindings()
        {
            var config =  await new RemoteConfig().GetConfig();
            
            Container.Bind<Config>().FromInstance(config).AsSingle();
        }
    }
}


