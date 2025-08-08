using Asteroids.Menu;
using Asteroids.SceneManage;
using System;
using UnityEngine;
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

