using System;
using Asteroids.Analytic;
using Zenject;

namespace Asteroids.Total.Installers
{ 
    public class AnalyticsControllerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<AnalyticsController>().AsSingle();
        }
        
    }
}

