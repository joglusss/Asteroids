using System;
using Asteroids.Ads;
using UnityEngine;
using Zenject;

namespace Asteroids.Total.Installers
{ 
    public class AdsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IAdsService>().To<AdsController>().AsSingle().NonLazy();
        }
    }
}


