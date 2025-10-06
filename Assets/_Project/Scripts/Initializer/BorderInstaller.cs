using System;
using System.ComponentModel;
using Asteroids.Helpers;
using Zenject;

namespace Asteroids.Installers
{ 
    public class BorderInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<BorderSetting>().AsSingle();
        }
        
    }
}


