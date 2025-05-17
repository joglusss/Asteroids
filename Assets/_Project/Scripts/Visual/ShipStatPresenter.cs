using Asteroids.SceneManage;
using UnityEngine;

namespace Asteroids.Visual
{
    public class ShipStatPresenter : IInitialize
    {
        private ShipStatModel model;
        private ShipStatView view;
        private ImmortalBlink viewImmortalBlink;

        public void Initialize(DependencyContainer dependencyContainer)
        {
            model = dependencyContainer.ShipStatModelLink;
            view = dependencyContainer.ShipStatViewLink;
            viewImmortalBlink = dependencyContainer.ShipLink.GetComponent<ImmortalBlink>();

            model.ChangedHealth += ChangeHealth;
            model.ChangedCoordinates += ChangeCoordinates;
            model.ChangedAngle += ChangeAngle;
            model.ChangedSpeed += ChangeSpeed;
            model.ChangedLaserCount += ChangeLaserCount;
            model.ChangedLaserCooldown += ChangeLaserCooldown;

            model.ChangedImmortality += viewImmortalBlink.SwitchBlinking;
        }

        public void ChangeHealth(int a) => view.ChangeHealth(a);

        public void ChangeCoordinates(Vector2 a) => view.ChangeCoordinates(a);

        public void ChangeAngle(float a) => view.ChangeAngle(a);

        public void ChangeSpeed(float a) => view.ChangeSpeed(a);

        public void ChangeLaserCount(int a) => view.ChangeLaserCount(a);

        public void ChangeLaserCooldown(float a) => view.ChangeLaserCooldown(a);

       
    }

}
