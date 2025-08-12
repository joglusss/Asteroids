using Zenject;

namespace Asteroids.Total.Installers
{ 
    public class AnalyticInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IAnalytic>().To<FirebaseAnalytic>().AsSingle().NonLazy();
        }
        
    }
}


