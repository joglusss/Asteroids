using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace Asteroids.Objects
{
    public class Laser : SpaceObject
    {
        [SerializeField] private float _lifeTime;

        protected Coroutine _coroutineLifetimeCounter;

        public override void Launch(Vector2 from, Vector2 direction)
        {
            transform.position = from;
            transform.up = direction;


            List<RaycastHit2D> results = new List<RaycastHit2D>();
            ContactFilter2D filter = new ContactFilter2D() { useTriggers = true };
            if (Physics2D.Raycast(from, direction, filter, results, float.MaxValue) > 0)
            {
                foreach (RaycastHit2D hit in results)
                {
                    if (hit.collider.TryGetComponent(out ISpaceInteract SpaceInteract))
                    {
                        SpaceInteract.Interact(SpaceObjectTypeEnum.SpaceShip);
                    }
                }
            }

            StartCoroutine(LifetimeCounter());
        }

        private IEnumerator LifetimeCounter()
        {
            yield return new WaitForSeconds(_lifeTime);

            _coroutineLifetimeCounter = null;
            _spaceObjectQueue.ReturnObject(this);
        }
    }

}
