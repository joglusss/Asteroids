using Asteroids.Objects;
using Asteroids.SceneManage;
using UnityEngine;
using System.Collections;
using Asteroids.Visual;
using Zenject;

namespace Asteroids.Ship 
{
    [RequireComponent(typeof(Collider2D))]
    public class ShipStat : MonoBehaviour, ISpaceInteract, IInitializable
    {
        [SerializeField] private int _startHealth;
        [SerializeField] private float _immortalityTime;

        private ShipStatModel _model;
        private Coroutine _frameCounter;
        private Collider2D _collider;

        [field: SerializeField] public SpaceObjectType SpaceObjectType { get; private set; }

        [Inject]
        private void Construct(ShipStatModel shipStatModel)
        {
            _model = shipStatModel;
            _collider = GetComponent<Collider2D>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<ISpaceInteract>(out ISpaceInteract spaceObject))
                Interact(spaceObject.SpaceObjectType);
        }

        public void Initialize()
        {
            _model.Health = _startHealth;
            _frameCounter = StartCoroutine(FrameCounter());
        }

        public void Interact(SpaceObjectType collisionSpaceObjectType)
        {
            if (_frameCounter == null && collisionSpaceObjectType != SpaceObjectType.SpaceShip)
            {
                _model.Health--;
                _frameCounter = StartCoroutine(FrameCounter());
            }
        }

        private IEnumerator FrameCounter()
        {
            _model.Immortality = true;
            _collider.enabled = false;
            yield return new WaitForSeconds(_immortalityTime);
            _collider.enabled = true;
            _frameCounter = null;
            _model.Immortality = false;
        }
    }
}

