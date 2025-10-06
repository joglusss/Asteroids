using System;
using Asteroids.Objects;
using Newtonsoft.Json;

namespace Asteroids.Total
{
    public class Config
    {
        [JsonProperty]
        public int MaxLaserCount { get; private set; } = 1;
        [JsonProperty]
        public float LaserCooldown { get; private set; } = 20;
        [JsonProperty]
        public int MaxHpCount { get; private set; } = 1;
        [JsonProperty]
        public float ImmortalityTime { get; private set; } = 1;

        [JsonProperty]
        public int AsteroidCost { get; private set; } = 2;
        [JsonProperty]
        public int AlienCost { get; private set; } = 3;
        [JsonProperty]
        public int SmallAsteroidCost { get; private set; } = 1;

        [JsonProperty]
        public LaunchCycleSetting Asteroids { get; private set; } = new() { StartObjectCount = 5, MaxTime = 10, MinTime = 8};
        [JsonProperty]
        public LaunchCycleSetting Alien { get; private set; } = new() { StartObjectCount = 0, MaxTime = 10, MinTime = 8};

        [JsonProperty]
        public float SpaceShipForwardSpeed { get; private set; } = 7.0f;
        [JsonProperty]
        public float SpaceShipAngularSpeed { get; private set; } = -1000.0f;
        [JsonProperty]
        public float AsteroidsSpeed { get; private set; } = 3;
        [JsonProperty]
        public float SmallAsteroidsSpeed { get; private set; } = 5;
        [JsonProperty]
        public float AlienSpeed { get; private set; } = 4;
        [JsonProperty]
        public float AlienAngularSpeed { get; private set; } = 4;
        [JsonProperty]
        public float BulletSpeed { get; private set; } = 15;
    }
}