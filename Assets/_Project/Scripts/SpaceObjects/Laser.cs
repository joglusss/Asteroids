using UnityEngine;
using System.Collections.Generic;

namespace Asteroids.Objects
{
    public class Laser : SpaceObject
    {
        protected RectTransform m_rectTransform;
        public override void OnStartGame()
        {
            m_rectTransform = GetComponent<RectTransform>();

        }

        public override void Launch(Vector2 from, Vector2 direction)
        {
            m_rectTransform.position = from;
            m_rectTransform.up = direction;


            List<RaycastHit2D> results = new List<RaycastHit2D>();
            ContactFilter2D filter = new ContactFilter2D() { useTriggers = true };
            if (Physics2D.Raycast(from, direction, filter, results, float.MaxValue) > 0)
            {
                foreach (RaycastHit2D hit in results)
                {
                    if (hit.collider.TryGetComponent(out ISpaceInteract SpaceInteract))
                    {
                        SpaceInteract.Interact(ISpaceInteract.SpaceObjectTypeEnum.SpaceShip);
                    }
                }
            }

            coroutineLifetimeCounter = StartCoroutine(LifetimeCounter());
        }


    }

}
