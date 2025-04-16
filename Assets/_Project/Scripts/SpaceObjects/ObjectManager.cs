using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;
using Asteroids.Visual;

namespace Asteroids.Objects
{
    public class ObjectManager : MonoBehaviour, IResetable
    {
        public bool IsOutCanvas(Vector2 position, out Vector2 wall)
        {

            wall = Vector2.zero;
            float deltaDistance = 0;

            if (position.x < 0.0f)
            {
                deltaDistance = -position.x;
                wall = Vector2.left;
            }

            if (position.x > canvasSize.x && deltaDistance < (position.x - canvasSize.x))
            {
                deltaDistance = position.x - canvasSize.x;
                wall = Vector2.right;
            }

            if (position.y < 0.0f && deltaDistance < -position.y)
            {
                deltaDistance = -position.y;
                wall = Vector2.down;
            }

            if (position.y > canvasSize.y && deltaDistance < (position.y - canvasSize.y))
            {
                wall = Vector2.up;
            }


            return wall != Vector2.zero;
        }

        public void BorderTeleport(Rigidbody2D rb, Vector2 wall)
        {
            if (wall == Vector2.left)
            {
                rb.position = BorderTeleportPosition(Vector2.zero, Vector2.up * canvasSize.y, rb.position);
                return;
            }
            if (wall == Vector2.right)
            {
                rb.position = BorderTeleportPosition(Vector2.right * canvasSize.x, canvasSize, rb.position);
                return;
            }
            if (wall == Vector2.down)
            {
                rb.position = BorderTeleportPosition(Vector2.zero, Vector2.right * canvasSize.x, rb.position);
                return;
            }
            if (wall == Vector2.up)
            {
                rb.position = BorderTeleportPosition(Vector2.up * canvasSize.y, canvasSize, rb.position);
                return;
            }

        }

        public Vector2 BorderTeleportPosition(Vector2 WallPoint1, Vector2 WallPoint2, Vector2 objectPoint)
        {

            Vector2 intersectPoint = Vector2.zero;
            Vector2 centerPoint = canvasSize / 2.0f;

            intersectPoint = LineIntersect(centerPoint, objectPoint, WallPoint1, WallPoint2);

            Vector2 dirToShip = objectPoint - centerPoint;
            Vector2 dirToIntersect = intersectPoint - centerPoint;

            float distanceOutScreen = dirToShip.magnitude - 2.0f * (dirToShip.magnitude - dirToIntersect.magnitude);

            Vector2 newObjectPosition = centerPoint + dirToShip.normalized * -distanceOutScreen;

            return newObjectPosition;
        }

        public static Vector2 LineIntersect(Vector2 A, Vector2 B, Vector2 C, Vector2 D)
        {
            float a1 = B.y - A.y;
            float b1 = A.x - B.x;
            float c1 = a1 * (A.x) + b1 * (A.y);

            float a2 = D.y - C.y;
            float b2 = C.x - D.x;
            float c2 = a2 * (C.x) + b2 * (C.y);

            float denominator = (D.y - C.y) * (B.x - A.x) - (D.x - C.x) * (B.y - A.y);

            if (denominator != 0f)
            {
                float x = (b2 * c1 - b1 * c2) / denominator;
                float y = (a1 * c2 - a2 * c1) / denominator;

                return new Vector2(x, y);
            }

            return Vector2.zero;

        }

        [System.Serializable]
        public class ObjectQueue
        {
            [SerializeField] private GameObject ObjectPrefab;

            public static Transform SpaceObjectContainer;

            private Queue<SpaceObject> queue;

            private event Action<SpaceObject> objectReturnToQueue;
            public event Action<SpaceObject> ObjectReturnToQueue { add => objectReturnToQueue += value; remove => objectReturnToQueue -= value; }

            public void Initialize()
            {

                if (ObjectPrefab == null)
                {
                    Debug.LogError("ObjectPrefab was not found");
                    return;
                }

                queue = new Queue<SpaceObject>();
            }

            private void AddNewObject()
            {
                if (!Instantiate(ObjectPrefab, SpaceObjectContainer).TryGetComponent(out SpaceObject newSpaceObject))
                {
                    Debug.LogError("ObjectPrefab doesn't have a component");
                    return;
                }

                Queue<SpaceObject> link = queue;

                newSpaceObject.ReturnDelegate = () => { link.Enqueue(newSpaceObject); newSpaceObject.gameObject.SetActive(false); objectReturnToQueue?.Invoke(newSpaceObject); };
                newSpaceObject.OnStartGame();
                newSpaceObject.gameObject.SetActive(false);
                queue.Enqueue(newSpaceObject);
            }

            public SpaceObject DrawObject()
            {
                if (queue.Count == 0)
                    AddNewObject();

                SpaceObject returnSpaceObject = queue.Dequeue();
                returnSpaceObject.gameObject.SetActive(true);
                return returnSpaceObject;
            }
        }

        [field: SerializeField] private RectTransform canvasRectTransformLink;
        [field: SerializeField] public Transform SpaceObjectContainer { get; private set; }

        public Vector2 canvasSize { get { return canvasRectTransformLink.sizeDelta * canvasRectTransformLink.lossyScale; } }
        public Vector2 speedScale { get { return canvasRectTransformLink.lossyScale; } }


        [field: SerializeField] public ObjectQueue bullet { get; private set; }
        [field: SerializeField] public ObjectQueue asteroid { get; private set; }
        [field: SerializeField] public ObjectQueue smallAsteroid { get; private set; }
        [field: SerializeField] public ObjectQueue alien { get; private set; }
        [field: SerializeField] public ObjectQueue laser { get; private set; }

        public void Init()
        {
            ObjectQueue.SpaceObjectContainer = SpaceObjectContainer;

            bullet.Initialize();
            asteroid.Initialize();
            smallAsteroid.Initialize();
            alien.Initialize();
            laser.Initialize();

            ((IResetable)this).InitialazeIRessetable();
        }

        private IEnumerator LaunchCycle(ObjectQueue objQueue, float minTime, float maxTime, int startCount = -1)
        {
            Vector2 centerPoint = canvasSize / 2.0f;


            void LaunchObject()
            {
                SpaceObject a = objQueue.DrawObject();

                if (a != null)
                {

                    Vector2 position = canvasSize + new Vector2(UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(-1.0f, 1.0f)) * canvasSize.magnitude;
                    IsOutCanvas(position, out Vector2 wallSide);

                    if (wallSide == Vector2.left)
                        position = LineIntersect(Vector2.zero, Vector2.up * canvasSize.y, position, centerPoint);
                    if (wallSide == Vector2.right)
                        position = LineIntersect(Vector2.right * canvasSize.x, canvasSize, position, centerPoint);
                    if (wallSide == Vector2.down)
                        position = LineIntersect(Vector2.zero, Vector2.right * canvasSize.x, position, centerPoint);
                    if (wallSide == Vector2.up)
                        position = LineIntersect(Vector2.up * canvasSize.y, canvasSize, position, centerPoint);

                    if (position == Vector2.zero)
                        Debug.LogWarning("LineIntersect is zero");

                    a.Launch(position, new Vector2(UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(-1.0f, 1.0f)));
                }
            }

            for (int i = 0; i < startCount; i++)
            {
                LaunchObject();
            }

            while (this.enabled)
            {
                LaunchObject();

                yield return new WaitForSeconds(UnityEngine.Random.Range(minTime, maxTime));
            }
        }

        public void StopGame()
        {
            StopAllCoroutines();
        }

        public void StartGame()
        {

            StartCoroutine(LaunchCycle(asteroid, 3, 10, 5));
            StartCoroutine(LaunchCycle(alien, 10, 15));
        }
    }


}
