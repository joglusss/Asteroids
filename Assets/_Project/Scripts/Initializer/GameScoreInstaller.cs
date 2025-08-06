using Asteroids.Score;
using UnityEngine;
using Zenject;

namespace Asteroids.Installers
{
	public class GameScoreInstaller : MonoInstaller
	{
		[SerializeField] private ScoreView _scoreView;
		[SerializeField] private ScoreCounter _scoreCounter;


		public override void InstallBindings()
		{
			Container.BindInterfacesAndSelfTo<ScoreModel>().AsSingle();
			Container.BindInterfacesAndSelfTo<ScoreViewModel>().AsSingle();
			Container.BindInterfacesTo<ScoreView>().FromInstance(_scoreView).AsSingle();
			
			Container.BindInterfacesAndSelfTo<ScoreCounter>().FromInstance(_scoreCounter).AsSingle();
		}   
	}
}
