using Asteroids.Ship;
using Asteroids.Visual;
using System;
using UnityEngine;
using Zenject;

namespace Asteroids.Installers
{
	public class ShipInstaller : MonoInstaller
	{
		[SerializeField] private ShipControl _shipControl;
		[SerializeField] private ShipStatView _shipStatView;
		[SerializeField] private ShipStatPreference _shipStatPreference;
		
		public override void InstallBindings()
		{
			Container.Bind<ShipStatPreference>().FromInstance(_shipStatPreference);

			Container.BindInterfacesAndSelfTo<ShipStatModel>().AsSingle().NonLazy();
			Container.BindInterfacesAndSelfTo<ShipStatViewModel>().AsSingle().NonLazy();
			
			Container.BindInterfacesAndSelfTo<ShipStatView>().FromInstance(_shipStatView).AsSingle();

			System.Type[] types = new System.Type[] 
			{   
				typeof(ShipAnimationControl),
				typeof(ShipControl), 
				typeof(ShipStat), 
				typeof(ShipWeapon),
				typeof(IInitializable), 
				typeof(IDisposable)
			};
			Container.Bind(types).FromComponentsInNewPrefab(_shipControl).AsSingle().Lazy();
		}
	}
}
