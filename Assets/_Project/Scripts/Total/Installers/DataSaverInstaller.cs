using Zenject;

namespace Asteroids.Total.Installers
{
    public class DataSaverInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IDataSaver>().To<LocalDataSaver>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<SaveService>().AsSingle().NonLazy();
        }
    }
}
