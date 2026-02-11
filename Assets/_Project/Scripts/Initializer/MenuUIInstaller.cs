using Asteroids.Menu;
using UnityEngine;
using Zenject;

namespace Asteroids.Installers
{
	public class MenuUIInstaller : MonoInstaller
	{
        [SerializeField] private Canvas _menuUI;

        public override void InstallBindings()
		{
            Container.Bind<MenuModel>().AsSingle();
			Container.BindInterfacesAndSelfTo<MenuViewModel>().AsSingle();

            Container.Bind<IInitializable>().FromComponentsInNewPrefab(_menuUI).AsTransient();
        }
	}
}

