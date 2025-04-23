using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;
using Asteroids.Visual;
using Asteroids.Helpers;

namespace Asteroids.Objects
{
    public class ObjectManager : MonoBehaviour
    {
        [field: SerializeField] private Transform _spaceObjectContainer;
        [field: SerializeField] private GameObject _bulletPrefab;
        [field: SerializeField] private GameObject _asteroidPrefab;
        [field: SerializeField] private GameObject _smallAsteroidPrefab;
        [field: SerializeField] private GameObject _alienPrefab;
        [field: SerializeField] private GameObject _laserPrefab;

        [field: SerializeField] public Transform PlayerShip { get; private set; }

        public SpaceObjectQueue BulletQueue { get; private set; }
        public SpaceObjectQueue AsteroidQueue { get; private set; }
        public SpaceObjectQueue SmallAsteroidQueue { get; private set; }
        public SpaceObjectQueue AlienQueue { get; private set; }
        public SpaceObjectQueue LaserQueue { get; private set; }
        public Vector2 CanvasSize { get; private set; }

        public void Init()
        {
            Camera camera = Camera.main;
            float halfHeight = camera.orthographicSize;
            float halfWidth = camera.aspect * halfHeight;

            camera.transform.position = new Vector3 (halfWidth, halfHeight, camera.transform.position.z);

            CanvasSize = new Vector2(halfWidth * 2.0f, halfHeight * 2.0f);

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
            StartCoroutine(LaunchCycle(AsteroidQueue, 3, 10, 5));
            StartCoroutine(LaunchCycle(AlienQueue, 10, 15));
        }

        private void LaunchObject(SpaceObject spaceObject)
        {
            Vector2 centerPoint = CanvasSize / 2.0f;

            Vector2 position = CanvasSize + new Vector2(UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(-1.0f, 1.0f)) * CanvasSize.magnitude;
            GameMath.IsOutCanvas(position, out Vector2 wallSide, CanvasSize);

            if (wallSide == Vector2.left)
                position = GameMath.LineIntersect(Vector2.zero, Vector2.up * CanvasSize.y, position, centerPoint);
            if (wallSide == Vector2.right)
                position = GameMath.LineIntersect(Vector2.right * CanvasSize.x, CanvasSize, position, centerPoint);
            if (wallSide == Vector2.down)
                position = GameMath.LineIntersect(Vector2.zero, Vector2.right * CanvasSize.x, position, centerPoint);
            if (wallSide == Vector2.up)
                position = GameMath.LineIntersect(Vector2.up * CanvasSize.y, CanvasSize, position, centerPoint);

            if (position == Vector2.zero)
                Debug.LogWarning("LineIntersect is zero");

            spaceObject.Launch(position, new Vector2(UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(-1.0f, 1.0f)));

        }

        private IEnumerator LaunchCycle(SpaceObjectQueue objQueue, float minTime, float maxTime, int startCount = -1)
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
