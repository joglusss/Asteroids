using Zenject;

namespace Asteroids.Total.Installers
{ 
    public class AnalyticInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IAnalyticsService>().To<FirebaseAnalytic>().AsSingle().NonLazy();
        }
        
    }
}


