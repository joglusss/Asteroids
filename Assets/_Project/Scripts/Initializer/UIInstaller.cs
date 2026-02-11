using Asteroids.Score;
using Asteroids.Ship;
using UnityEngine;
using Zenject;

namespace Asteroids.Installers
{
	public class UIInstaller : MonoInstaller
	{
		[SerializeField] private ShipStatView _inGameUI;

		public override void InstallBindings()
		{
			Container.BindInterfacesAndSelfTo<ScoreModel>()
				.AsSingle();
			Container.BindInterfacesAndSelfTo<ScoreViewModel>()
				.AsSingle(); 
			Container.BindInterfacesAndSelfTo<ScoreCounter>()
				.AsSingle();

            Container.BindInterfacesAndSelfTo<ShipStatModel>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ShipStatViewModel>().AsSingle().NonLazy();

            Container.Bind<IInitializable>().FromComponentsInNewPrefab(_inGameUI).AsTransient();
        }   
	}
}
