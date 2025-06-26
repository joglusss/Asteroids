using System;
using UnityEngine;

namespace Asteroids.Input
{
    public interface IInput
    {
        public event Action<Vector2> MoveEvent;
        public event Action LaserShotEvent;
        public event Action BulletShotEvent;
        public event Action EscapeEvent;
    }
}
