using UnityEngine;
using System.Collections;
using Asteroids.Helpers;
using Zenject;
using Asteroids.Ship;

namespace Asteroids.Objects
{
    public class ObjectManager : MonoBehaviour, IInitializable
    {
        [field: SerializeField] private LaunchCycleSetting _asteroidLaunchCycleSetting;
        [field: SerializeField] private LaunchCycleSetting _alienLaunchCycleSetting;

        public Transform AlienTarget { get; private set; }
        public SpaceObjectQueue BulletQueue { get; private set; }
        public SpaceObjectQueue AsteroidQueue { get; private set; }
        public SpaceObjectQueue SmallAsteroidQueue { get; private set; }
        public SpaceObjectQueue AlienQueue { get; private set; }
        public SpaceObjectQueue LaserQueue { get; private set; }
        public Vector2 BorderSize { get; private set; }
        public Vector2[] BorderPoints { get; private set; }
        public Vector2 BorderCenter { get; private set; }

        public void Initialize()
        {
            BorderSize = GameMath.CalculateBorderSize();
            BorderPoints = GameMath.CalculateBorderPoints();
            BorderCenter = GameMath.BorderCenter();

            StartCoroutine(LaunchCycle(AsteroidQueue, _asteroidLaunchCycleSetting));
            StartCoroutine(LaunchCycle(AlienQueue, _alienLaunchCycleSetting));
        }

        [Inject]
        private void Construct(ShipControl shipControl, [Inject(Id = "Bullet")] SpaceObjectQueue bullet, [Inject(Id = "Asteroid")] SpaceObjectQueue asteroid, [Inject(Id = "SmallAsteroid")] SpaceObjectQueue smallAsteroid, [Inject(Id = "Alien")] SpaceObjectQueue alien, [Inject(Id = "Laser")] SpaceObjectQueue laser)
        {
            AlienTarget = shipControl.transform;

            BulletQueue = bullet;
            AsteroidQueue = asteroid;
            SmallAsteroidQueue = smallAsteroid;
            AlienQueue = alien;
            LaserQueue = laser;
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        private void LaunchObject(SpaceObject spaceObject)
        {
            Vector2 position = BorderSize + new Vector2(UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(-1.0f, 1.0f)) * BorderSize.magnitude;
            position = GameMath.TeleportToBorder(position, BorderCenter, BorderPoints);
            spaceObject.Launch(position, new Vector2(UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(-1.0f, 1.0f)));
        }

        private IEnumerator LaunchCycle(SpaceObjectQueue objQueue, LaunchCycleSetting setting)
        {
            return LaunchCycle(objQueue, setting.MinTime, setting.MaxTime, setting.StartObjectCount);
        }

        private IEnumerator LaunchCycle(SpaceObjectQueue objQueue, float minTime, float maxTime, int startCount)
        {
            for (int i = 0; i < startCount; i++)
            {
                LaunchObject(objQueue.DrawObject());
            }

            while (enabled)
            {
                LaunchObject(objQueue.DrawObject());

                yield return new WaitForSeconds(UnityEngine.Random.Range(minTime, maxTime));
            }
        }
    }
}
