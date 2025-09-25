using UnityEngine;
using System.Collections;
using Asteroids.Helpers;
using Zenject;
using Asteroids.Ship;
using Asteroids.Asset;

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

        private IAssetContainer<GameObject> _asteroid;
        private ShipStatViewModel _shipStatVM;

        [Inject]
        private void Construct(
            ShipControl shipControl,
            ShipStatViewModel shipStatVM,
            [Inject(Id = SpaceObjectID.Bullet)] IAssetContainer<GameObject> bullet,
            [Inject(Id = SpaceObjectID.Asteroid)] IAssetContainer<GameObject> asteroid,
            [Inject(Id = SpaceObjectID.SmallAsteroid)] IAssetContainer<GameObject> smallAsteroid,
            [Inject(Id = SpaceObjectID.Alien)] IAssetContainer<GameObject> alien,
            [Inject(Id = SpaceObjectID.Laser)] IAssetContainer<GameObject> laser)
        {
            AlienTarget = shipControl.transform;

            _asteroid = asteroid;
            _shipStatVM = shipStatVM;
            
            AsteroidQueue = new SpaceObjectQueue(asteroid.LoadAssetSync().GetComponent<SpaceObject>(), this);
            BulletQueue = new SpaceObjectQueue(bullet.LoadAssetSync().GetComponent<SpaceObject>(), this);
            SmallAsteroidQueue = new SpaceObjectQueue(smallAsteroid.LoadAssetSync().GetComponent<SpaceObject>(), this);
            AlienQueue = new SpaceObjectQueue(alien.LoadAssetSync().GetComponent<SpaceObject>(), this);
            LaserQueue = new SpaceObjectQueue(laser.LoadAssetSync().GetComponent<SpaceObject>(), this);
        }

        public void Initialize()
        {
            BorderSize = GameMath.CalculateBorderSize();
            BorderPoints = GameMath.CalculateBorderPoints();
            BorderCenter = GameMath.BorderCenter();
            
            StartCoroutine(LaunchCycle(AsteroidQueue, _asteroidLaunchCycleSetting));
            StartCoroutine(LaunchCycle(AlienQueue, _alienLaunchCycleSetting));
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

            yield return null;
            
            while (enabled)
            {
                if(_shipStatVM.LifeStatus.CurrentValue)
                    LaunchObject(objQueue.DrawObject());

                yield return new WaitForSeconds(UnityEngine.Random.Range(minTime, maxTime));
            }
        }
    }
}
