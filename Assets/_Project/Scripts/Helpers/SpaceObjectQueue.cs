using Asteroids.Objects;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Asteroids.Objects
{
    [System.Serializable]
    public class SpaceObjectQueue
    {
        private ObjectManager _objectManager;
        private GameObject _objectPrefab;
        private Transform _objectsContainer;
        private Queue<SpaceObject> _queue;

        public event Action<SpaceObject> ObjectReturnToQueue { add => objectReturnToQueue += value; remove => objectReturnToQueue -= value; }

        private event Action<SpaceObject> objectReturnToQueue;

        public SpaceObjectQueue(GameObject prefab, Transform container, ObjectManager objectManager)
        {
            _objectManager = objectManager;
            _objectPrefab = prefab;
            _objectsContainer = container;
            _queue = new Queue<SpaceObject>();

            AddNewObject();
        }

        public SpaceObject DrawObject()
        {
            if (_queue.Count == 0)
                AddNewObject();

            SpaceObject returnSpaceObject = _queue.Dequeue();
            returnSpaceObject.gameObject.SetActive(true);
            return returnSpaceObject;
        }

        public void ReturnObject(SpaceObject spaceObject) 
        {
            _queue.Enqueue(spaceObject);
            spaceObject.gameObject.SetActive(false); 
            objectReturnToQueue?.Invoke(spaceObject);
        }

        private void AddNewObject()
        {
            if (!MonoBehaviour.Instantiate(_objectPrefab, _objectsContainer).TryGetComponent(out SpaceObject newSpaceObject))
            {
                Debug.LogError("ObjectPrefab doesn't have a component");
                return;
            }

            newSpaceObject.Initialize(_objectManager, this);

            newSpaceObject.gameObject.SetActive(false);
            _queue.Enqueue(newSpaceObject);
        }
    }
}