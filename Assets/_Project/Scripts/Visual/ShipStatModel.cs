using Asteroids.SceneManage;
using System;
using UnityEngine;

namespace Asteroids.Visual
{
    public class ShipStatModel
    {
        public event Action<bool> ChangedImmortality;
        public event Action<int> ChangedHealth;
        public event Action<Vector2> ChangedCoordinates;
        public event Action<float> ChangedAngle;
        public event Action<float> ChangedSpeed;
        public event Action<int> ChangedLaserCount;
        public event Action<float> ChangedLaserCooldown;

        public bool Immortality
        {
            get => _immortality;
            set
            {
                _immortality = value;
                ChangedImmortality?.Invoke(value);
            }
        }
        public int Health
        {
            get => _health;
            set
            {
                if (_immortality) return;
                _health = Mathf.Clamp(value, 0, int.MaxValue);
                ChangedHealth?.Invoke(value);
            }
        }
        public Vector2 Coordinate
        {
            get => _coordinate;
            set
            {
                _coordinate = value;
                ChangedCoordinates?.Invoke(value);
            }
        }
        public float Angle
        {
            get => _angle;
            set
            {
                _angle = value;
                ChangedAngle?.Invoke(value);
            }
        }
        public float Speed
        {
            get => _speed;
            set
            {
                _speed = value;
                ChangedSpeed?.Invoke(value);
            }
        }
        public int LaserCount
        {
            get => _laserCount;
            set
            {
                _laserCount = value;
                ChangedLaserCount?.Invoke(value);
            }
        }
        public float LaserCooldown
        {
            get => _laserCooldown;
            set
            {
                _laserCooldown = value;
                ChangedLaserCooldown?.Invoke(value);
            }
        }

        private bool _immortality;
        private int _health;
        private Vector2 _coordinate;
        private float _angle;
        private float _speed;
        private int _laserCount;
        private float _laserCooldown;
    }
}
