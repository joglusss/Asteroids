using System.Collections.Generic;
using System;
using UnityEngine;
using Unity.VisualScripting;

namespace Asteroids.Objects
{
    [System.Serializable]
    public class SpaceObjectQueue
    {
        private readonly ObjectManager _objectManager;
        private readonly SpaceObject _spaceObjectPrefab;
        private readonly Transform _objectsContainer;
        private readonly Queue<SpaceObject> _queue;

        public event Action<SpaceObject> ObjectReturnedToQueue;

        public SpaceObjectQueue(SpaceObject prefab, Transform container, ObjectManager objectManager)
        {
            _objectManager = objectManager;
            _spaceObjectPrefab = prefab;
            _objectsContainer = container;
            _queue = new Queue<SpaceObject>();
        }

        public SpaceObject DrawObject()
        {
            SpaceObject returnSpaceObject;

            if (_queue.Count == 0)
                returnSpaceObject = CreateNewObject();
            else
                returnSpaceObject = _queue.Dequeue();

            returnSpaceObject.gameObject.SetActive(true);
            return returnSpaceObject;
        }

        public void ReturnObject(SpaceObject spaceObject) 
        {
            if (_queue.Contains(spaceObject) || spaceObject == null || spaceObject.IsDestroyed())
                return;

            _queue.Enqueue(spaceObject);
            spaceObject.gameObject.SetActive(false);
            ObjectReturnedToQueue?.Invoke(spaceObject);
        }

        private SpaceObject CreateNewObject()
        {
            SpaceObject newSpaceObject = UnityEngine.Object.Instantiate(_spaceObjectPrefab, _objectsContainer);
            newSpaceObject.Initialize(_objectManager, this);

            return newSpaceObject;
        }
    }
}