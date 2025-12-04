using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using R3;

namespace Asteroids.Objects
{
    [System.Serializable]
    public class SpaceObjectQueue
    {
        private readonly SpaceObject _spaceObjectPrefab;
        private readonly Transform _objectsContainer;
        private readonly Queue<SpaceObject> _queue;
        
        public readonly ReactiveCommand<SpaceObject> ObjectReturned = new();
        public readonly ReactiveCommand<SpaceObject> ObjectDrawn = new();

        public SpaceObjectQueue(SpaceObject prefab, Transform objectsContainer)
        {
            _spaceObjectPrefab = prefab;
            _objectsContainer = objectsContainer;
            _queue = new Queue<SpaceObject>();
        }

        public SpaceObject DrawObject()
        {
            SpaceObject returnSpaceObject;

            if (_queue.Count == 0)
                returnSpaceObject = CreateNewObject();
            else
                returnSpaceObject = _queue.Dequeue();

            ObjectDrawn.Execute(returnSpaceObject);
            
            returnSpaceObject.gameObject.SetActive(true);
            return returnSpaceObject;
        }

        private void ReturnObject(SpaceObject spaceObject) 
        {
            if (_queue.Contains(spaceObject) || spaceObject == null || spaceObject.IsDestroyed())
                return;

            _queue.Enqueue(spaceObject);
            spaceObject.gameObject.SetActive(false);
            ObjectReturned.Execute(spaceObject);
        }

        private SpaceObject CreateNewObject()
        {
            SpaceObject newSpaceObject = Object.Instantiate(_spaceObjectPrefab, _objectsContainer);

            newSpaceObject.OnLifeEnd.Subscribe(x => ReturnObject(x)).AddTo(newSpaceObject);

            return newSpaceObject;
        }
    }
}