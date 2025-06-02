using Asteroids.Score;
using UnityEngine;
using Zenject;

namespace Asteroids.Installers
{
    public class ScoreInstaller : MonoInstaller
    {
        [SerializeField] private BestScoreView _bestScoreView;
        [SerializeField] private LastScoreView _lastScoreView;
        [SerializeField] private ScoreCounter _scoreCounter;
        [SerializeField] private ScoreCounterView _scoreCounterView;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ScoreManager>().FromNew().AsSingle();

            if (_scoreCounter != null)
            {
                Container.BindInterfacesAndSelfTo<ScoreCounter>().FromInstance(_scoreCounter).AsSingle();
                Container.BindInterfacesTo<ScoreCounterView>().FromInstance(_scoreCounterView);
            }

            if (_bestScoreView != null)
                Container.BindInterfacesTo<BestScoreView>().FromInstance(_bestScoreView);

            if (_lastScoreView != null)
                Container.BindInterfacesTo<LastScoreView>().FromInstance(_lastScoreView);
        }   
    }
}
