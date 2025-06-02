using Asteroids.SceneManage;
using Asteroids.Ship;
using UnityEngine;
using Zenject;

namespace Asteroids.Ship
{
    public class ShipStatPresenter : IInitializable, ILateDisposable
    {
        private ShipStatModel _model;
        private ShipStatView _view;
        private ImmortalBlink _viewImmortalBlink;
        private GameSceneContainerHandler _sceneContainerHandler;

        public void Initialize()
        {
            _model.ChangedHealth += ChangeHealth;
            _model.ChangedCoordinates += ChangeCoordinates;
            _model.ChangedAngle += ChangeAngle;
            _model.ChangedSpeed += ChangeSpeed;
            _model.ChangedLaserCount += ChangeLaserCount;
            _model.ChangedLaserCooldown += ChangeLaserCooldown;

            _model.ChangedImmortality += _viewImmortalBlink.SwitchBlinking;

            _model.ChangedLifeStatus += ChangeLifeStatus;
        }

        public void LateDispose()
        {
            _model.ChangedHealth -= ChangeHealth;
            _model.ChangedCoordinates -= ChangeCoordinates;
            _model.ChangedAngle -= ChangeAngle;
            _model.ChangedSpeed -= ChangeSpeed;
            _model.ChangedLaserCount -= ChangeLaserCount;
            _model.ChangedLaserCooldown -= ChangeLaserCooldown;

            _model.ChangedImmortality -= _viewImmortalBlink.SwitchBlinking;

            _model.ChangedLifeStatus -= ChangeLifeStatus;
        }

        [Inject]
        private void Construct(ShipStatModel shipStatModel, ShipStatView shipStatView, ShipControl shipControl, GameSceneContainerHandler sceneContainerHandler)
        {
            _model = shipStatModel;
            _view = shipStatView;
            _viewImmortalBlink = shipControl.GetComponent<ImmortalBlink>();
            _sceneContainerHandler = sceneContainerHandler;
        }

        public void ChangeHealth(int a) => _view.ChangeHealth(a);

        public void ChangeCoordinates(Vector2 a) => _view.ChangeCoordinates(a);

        public void ChangeAngle(float a) => _view.ChangeAngle(a);

        public void ChangeSpeed(float a) => _view.ChangeSpeed(a);

        public void ChangeLaserCount(int a) => _view.ChangeLaserCount(a);

        public void ChangeLaserCooldown(float a) => _view.ChangeLaserCooldown(a);

        public void ChangeLifeStatus(bool a) 
        {
            if (!a)
                _sceneContainerHandler.GoToMenu();
        }
    }

}
