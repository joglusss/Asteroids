using Zenject;

namespace Asteroids.Total.Installers
{ 
    public class ConfigInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
           Container.BindInterfacesAndSelfTo<RemoteConfig>().AsSingle().NonLazy(); 
        } 
    }
}