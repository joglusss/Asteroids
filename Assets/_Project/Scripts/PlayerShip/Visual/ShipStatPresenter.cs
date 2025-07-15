using Asteroids.SceneManage;
using UnityEngine;
using Zenject;
using R3;
using System;


namespace Asteroids.Ship
{
    public class ShipStatPresenter : IInitializable, IDisposable
    {
        private ShipStatModel _model;
        private ShipStatView _view;
        private CompositeDisposable _compositeDisposable = new CompositeDisposable();

        [Inject]
        private void Construct(ShipStatModel shipStatModel, ShipStatView shipStatView)
        {
            _model = shipStatModel;
            _view = shipStatView;
        }

        public void Initialize()
        {
            _model.HealthSubscribe.Subscribe(ChangeHealth).AddTo(_compositeDisposable);
            _model.CoordinatesSubscribe.Subscribe(ChangeCoordinates).AddTo(_compositeDisposable);
            _model.AngleSubscribe.Subscribe(ChangeAngle).AddTo(_compositeDisposable);
            _model.SpeedSubscribe.Subscribe(ChangeSpeed).AddTo(_compositeDisposable);
            _model.LaserCountSubscribe.Subscribe(ChangeLaserCount).AddTo(_compositeDisposable);
            _model.LaserCooldownSubscribe.Subscribe(ChangeLaserCooldown).AddTo(_compositeDisposable);
        }

        public void Dispose() => _compositeDisposable.Dispose();

        private void ChangeHealth(int a) => _view.ChangeHealth(a);

        private void ChangeCoordinates(Vector2 a) => _view.ChangeCoordinates(a);

        private void ChangeAngle(float a) => _view.ChangeAngle(a);

        private void ChangeSpeed(float a) => _view.ChangeSpeed(a);

        private void ChangeLaserCount(int a) => _view.ChangeLaserCount(a);

        private void ChangeLaserCooldown(float a) => _view.ChangeLaserCooldown(a);
    }

}
