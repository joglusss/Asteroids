using Asteroids.SceneManage;
using UnityEngine;
using Zenject;
using R3;


namespace Asteroids.Ship
{
    public class ShipStatPresenter : IInitializable, ILateDisposable
    {
        private ShipStatModel _model;
        private ShipStatView _view;
        private CompositeDisposable _compositeDisposable = new CompositeDisposable();

        public void Initialize()
        {
            _model.HealthSubscribe.Subscribe(ChangeHealth).AddTo(_compositeDisposable);
            _model.CoordinatesSubscribe.Subscribe(ChangeCoordinates).AddTo(_compositeDisposable);
            _model.AngleSubscribe.Subscribe(ChangeAngle).AddTo(_compositeDisposable);
            _model.SpeedSubscribe.Subscribe(ChangeSpeed).AddTo(_compositeDisposable);
            _model.LaserCountSubscribe.Subscribe(ChangeLaserCount).AddTo(_compositeDisposable);
            _model.LaserCooldownSubscribe.Subscribe(ChangeLaserCooldown).AddTo(_compositeDisposable);
        }

        public void LateDispose() => _compositeDisposable.Dispose();

        [Inject]
        private void Construct(ShipStatModel shipStatModel, ShipStatView shipStatView)
        {
            _model = shipStatModel;
            _view = shipStatView;
        }

        public void ChangeHealth(int a) => _view.ChangeHealth(a);

        public void ChangeCoordinates(Vector2 a) => _view.ChangeCoordinates(a);

        public void ChangeAngle(float a) => _view.ChangeAngle(a);

        public void ChangeSpeed(float a) => _view.ChangeSpeed(a);

        public void ChangeLaserCount(int a) => _view.ChangeLaserCount(a);

        public void ChangeLaserCooldown(float a) => _view.ChangeLaserCooldown(a);
    }

}
