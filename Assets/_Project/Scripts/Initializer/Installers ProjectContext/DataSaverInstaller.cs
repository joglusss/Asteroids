using Zenject;

namespace Asteroids.Total.Installers
{
    public class DataSaverInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IDataSaver>().WithId(SaverType.Local).To<LocalDataSaver>().AsCached().NonLazy();
            Container.Bind<IDataSaver>().WithId(SaverType.Cloud).To<CloudDataSaver>().AsCached().NonLazy();
            
            Container.BindInterfacesAndSelfTo<SaveService>().AsSingle().NonLazy();
        }
    }
}
