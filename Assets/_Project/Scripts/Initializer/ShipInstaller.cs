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
		[SerializeField] private ShipStatPreference _shipStatPreference;
		
		public override void InstallBindings()
		{
			Container.Bind<ShipStatPreference>().FromInstance(_shipStatPreference);

			System.Type[] types = new System.Type[] 
			{   
				typeof(ShipAnimationControl),
				typeof(ShipControl), 
				typeof(ShipStat), 
				typeof(ShipWeapon)
            };
			Container.Bind(types).FromComponentsInNewPrefab(_shipControl).AsSingle();

			foreach (var type in types) 
				Container.BindInterfacesTo(type).FromResolve().AsTransient();
        }
	}
}
