using Asteroids.Menu;
using Zenject;

namespace Asteroids.Installers
{
	public class MenuUIInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container.Bind<MenuModel>().AsSingle();
			Container.BindInterfacesAndSelfTo<MenuViewModel>().AsSingle();
		}
	}
}

