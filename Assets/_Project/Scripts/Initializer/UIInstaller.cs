using Asteroids.Score;
using UnityEngine;
using Zenject;

namespace Asteroids.Installers
{
	public class UIInstaller : MonoInstaller
	{
		[SerializeField] private ScoreView _scoreView;
		[SerializeField] private ScoreCounter _scoreCounter;


		public override void InstallBindings()
		{
			Container.BindInterfacesAndSelfTo<ScoreModel>().AsSingle();
			Container.BindInterfacesAndSelfTo<ScoreViewModel>().AsSingle(); 
			
			Container.BindInterfacesAndSelfTo<ScoreCounter>().FromInstance(_scoreCounter).AsSingle();
		}   
	}
}
