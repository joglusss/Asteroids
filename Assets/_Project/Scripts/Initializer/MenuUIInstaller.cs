using Asteroids.Menu;
using Asteroids.SceneManage;
using System;
using UnityEngine;
using Zenject;

namespace Asteroids.Installers
{
	public class MenuUIInstaller : MonoInstaller
	{
		[SerializeField] private MenuView _menuView;

		public override void InstallBindings()
		{
			Container.Bind<MenuModel>().AsSingle();
			Container.BindInterfacesAndSelfTo<MenuViewModel>().AsSingle();
			
			System.Type[] types = new System.Type[]
			{
				typeof(MenuView),
				typeof(IInitializable),
				typeof(IDisposable)
			};
			Container.Bind(types).FromInstance(_menuView).AsSingle();
		}
	}
}

