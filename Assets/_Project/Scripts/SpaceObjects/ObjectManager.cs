using UnityEngine;
using System.Collections;
using Asteroids.Helpers;
using Asteroids.SceneManage;

namespace Asteroids.Objects
{
    public class ObjectManager : MonoBehaviour, IInitialize
    {
        [field: SerializeField] private SpaceObject _bulletPrefab;
        [field: SerializeField] private SpaceObject _asteroidPrefab;
        [field: SerializeField] private SpaceObject _smallAsteroidPrefab;
        [field: SerializeField] private SpaceObject _alienPrefab;
        [field: SerializeField] private SpaceObject _laserPrefab;
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

        private Transform _spaceObjectContainer;

        public void Initialize(DependencyContainer dependencyContainer)
        {
            AlienTarget = dependencyContainer.ShipLink.transform;

            BorderSize = GameMath.CalculateBorderSize();
            BorderPoints = GameMath.CalculateBorderPoints();
            BorderCenter = GameMath.BorderCenter();

            _spaceObjectContainer = new GameObject() { name = "SpaceObjectContainer" }.transform;

            BulletQueue = new SpaceObjectQueue(_bulletPrefab, _spaceObjectContainer, this);
            AsteroidQueue = new SpaceObjectQueue(_asteroidPrefab, _spaceObjectContainer, this);
            SmallAsteroidQueue = new SpaceObjectQueue(_smallAsteroidPrefab, _spaceObjectContainer, this);
            AlienQueue = new SpaceObjectQueue(_alienPrefab, _spaceObjectContainer, this);
            LaserQueue = new SpaceObjectQueue(_laserPrefab, _spaceObjectContainer, this);
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        private void OnEnable()
        {
            StartCoroutine(LaunchCycle(AsteroidQueue, _asteroidLaunchCycleSetting));
            StartCoroutine(LaunchCycle(AlienQueue, _alienLaunchCycleSetting));
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

            while (this.enabled)
            {
                LaunchObject(objQueue.DrawObject());

                yield return new WaitForSeconds(UnityEngine.Random.Range(minTime, maxTime));
            }
        }
    }
}
