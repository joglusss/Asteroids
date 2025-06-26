using Asteroids.Score;
using UnityEngine;
using Zenject;

namespace Asteroids.Installers
{
    public class GameScoreInstaller : MonoInstaller
    {
        [SerializeField] private LastScoreView _lastScoreView;
        [SerializeField] private ScoreCounter _scoreCounter;
        [SerializeField] private ScoreCounterView _scoreCounterView;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ScoreCounter>().FromInstance(_scoreCounter).AsSingle();
            Container.BindInterfacesTo<ScoreCounterView>().FromInstance(_scoreCounterView);
            Container.BindInterfacesTo<LastScoreView>().FromInstance(_lastScoreView);
        }   
    }
}
