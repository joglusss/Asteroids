using UnityEngine;
using Zenject;

namespace Asteroids.Total.Installers
{
    public class DataSaverInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<DataHandler>().AsSingle();
        }
    }
}
