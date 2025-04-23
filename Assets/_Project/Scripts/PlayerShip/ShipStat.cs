using Asteroids.Objects;
using Asteroids.Visual;
using UnityEngine;
using System.Collections;
using System;

namespace Asteroids.Ship 
{
    [RequireComponent(typeof(Collider2D))]
    public class ShipStat : MonoBehaviour, ISpaceInteract
    {
        public event Action<int> HealthChanged { add => _healthChanged += value; remove => _healthChanged -= value;}

        private event Action<int> _healthChanged;

        [SerializeField] private Menu _menuScript;
        [SerializeField] private int _maxHealth;
        [SerializeField] private float _immortalityTime;

        private Coroutine _frameCounter;
        private Collider2D _collider;

        [field: SerializeField] public SpaceObjectTypeEnum SpaceObjectType { get; private set; }

        public bool IsImmortal => _frameCounter != null;
        public int HP { get; private set; }

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
        }

        private void OnEnable()
        {
            _frameCounter = StartCoroutine(FrameCounter());
            HP = _maxHealth;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<ISpaceInteract>(out ISpaceInteract spaceObject))
                Interact(spaceObject.SpaceObjectType);
        }

        public void Interact(SpaceObjectTypeEnum collisionSpaceObjectType)
        {
            if (_frameCounter == null && collisionSpaceObjectType != SpaceObjectTypeEnum.SpaceShip)
            {
                if (HP > 0)
                    _frameCounter = StartCoroutine(FrameCounter());

                HP--;
                _healthChanged?.Invoke(HP);

                if (HP == 0)
                    _menuScript.GoToMenu();
            }
        }

        private IEnumerator FrameCounter()
        {
            _collider.enabled = false;
            yield return new WaitForSeconds(_immortalityTime);
            _collider.enabled = true;
            _frameCounter = null;
        }
    }
}

