using Zenject;

namespace Asteroids.Total.Installers
{ 
    public class AnalyticInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<FirebaseAnalytic>().AsSingle().NonLazy();
        }
        
    }
}


