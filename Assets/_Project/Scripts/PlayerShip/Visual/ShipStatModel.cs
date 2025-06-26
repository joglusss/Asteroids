using Asteroids.SceneManage;
using R3;
using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using Zenject;

namespace Asteroids.Ship
{
    public class ShipStatModel
    {
        public Observable<int> HealthSubscribe => _health;
        public Observable<bool> ImmortalitySubscribe => _immortality;
        public Observable<Vector2> CoordinatesSubscribe => _coordinates;
        public Observable<float> AngleSubscribe => _angle;
        public Observable<float> SpeedSubscribe => _speed;
        public Observable<int> LaserCountSubscribe => _laserCount;
        public Observable<float> LaserCooldownSubscribe => _laserCooldown;
        public Observable<bool> LifeStatusSubscribe => _lifeStatus;

        private ShipAnimationControl _shipAnimationControl;
        private readonly ReactiveProperty<int> _health = new(1);
        private readonly ReactiveProperty<bool> _immortality = new(false);
        private readonly ReactiveProperty<Vector2> _coordinates = new();
        private readonly ReactiveProperty<float> _angle = new();
        private readonly ReactiveProperty<float> _speed = new();
        private readonly ReactiveProperty<int> _laserCount = new();
        private readonly ReactiveProperty<float> _laserCooldown = new();
        private readonly ReactiveProperty<bool> _lifeStatus = new(true);

        public int Health
        {
            get => _health.Value;
            set
            {
                if (_immortality.Value) return;
                _health.Value = Math.Clamp(value, 0, int.MaxValue);
                LifeStatus = value > 0;
            }
        }

        public bool Immortality
        {
            get => _immortality.Value;
            set
            {
                _immortality.Value = value;
                _shipAnimationControl.SwitchBlinking(value);
            }
        }

        public Vector2 Coordinates
        { 
            get => _coordinates.Value;
            set => _coordinates.Value = value;
        }

        public float Angle
        { 
            get => _angle.Value;
            set => _angle.Value = value;
        }

        public float Speed
        { 
            get => _speed.Value;
            set => _speed.Value = value;
        }

        public int LaserCount
        {
            get => _laserCount.Value;
            set => _laserCount.Value = value;
        }

        public float LaserCooldown
        { 
            get => _laserCooldown.Value;
            set => _laserCooldown.Value = value;
        }

        public bool LifeStatus
        {
            get => _lifeStatus.Value;
            set 
            {
                _lifeStatus.Value = value;
                if (!value) _shipAnimationControl.Death();
            }
        }

        [Inject]
        public void Construct(ShipAnimationControl shipAnimationControl)
        {
            _shipAnimationControl = shipAnimationControl;
        }
    }
}
