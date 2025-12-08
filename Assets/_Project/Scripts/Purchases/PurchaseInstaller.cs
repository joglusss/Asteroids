using Zenject;

namespace Asteroids.Total.Installers
{
    public class PurchaseInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PurchasesService>().AsSingle();
            Container.BindInterfacesTo<IAPurchase>().AsSingle();
        }
    }
}
