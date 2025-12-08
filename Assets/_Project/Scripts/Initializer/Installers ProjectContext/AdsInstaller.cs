using Asteroids.Ads;
using Zenject;

namespace Asteroids.Total.Installers
{ 
    public class AdsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<AdsController>().AsSingle().NonLazy();
        }
    }
}


