using Asteroids.Objects;
using UnityEngine;
using Zenject;
using System;
using R3;

namespace Asteroids.Ship 
{
	[RequireComponent(typeof(Collider2D))]
	public class ShipStat : MonoBehaviour, ISpaceInteract, IInitializable, IDisposable
	{
		[SerializeField] private int _startHealth;
		[SerializeField] private float _immortalityTime;

		private ShipStatViewModel _viewModel;
		private Coroutine _frameCounter;
		private Collider2D _collider;
		private CompositeDisposable _disposables = new CompositeDisposable();

		[field: SerializeField] public SpaceObjectType SpaceObjectType { get; private set; }

		[Inject]
		private void Construct(ShipStatViewModel viewModel)
		{
			_viewModel = viewModel;
			_collider = GetComponent<Collider2D>();
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.TryGetComponent<ISpaceInteract>(out ISpaceInteract spaceObject))
				Interact(spaceObject.SpaceObjectType);
		}

		public void Initialize()
		{
			_viewModel.Immortality.Subscribe(SwitchCollision).AddTo(_disposables);
		}

		public void Dispose()
		{
			_disposables.Dispose();
		}

		public void Interact(SpaceObjectType collisionSpaceObjectType)
		{
			if (_viewModel.IsImmortal() == false && collisionSpaceObjectType != SpaceObjectType.SpaceShip)
			{
				_viewModel.AddHealth(-1);
			}
		}

		private void SwitchCollision(bool value) => _collider.enabled = !value;
	}
}

