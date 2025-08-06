using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace Asteroids.Ship
{
	public class ShipStatView : MonoBehaviour, IInitializable
	{
		// [SerializeField] UIDocument _uIDocument;
		
		[SerializeField] private TMP_Text _healthText;
		[SerializeField] private TMP_Text _coordinatesText;
		[SerializeField] private TMP_Text _angleText;
		[SerializeField] private TMP_Text _speedText;
		[SerializeField] private TMP_Text _laserCountText;
		[SerializeField] private TMP_Text _laserCooldownText;

		private ShipStatViewModel _viewModel;
		private CompositeDisposable _disposable = new();

		[Inject]
		private void Construct(ShipStatViewModel viewModel)
		{ 
			_viewModel = viewModel;
		}

		public void Initialize()
		{
			// _healthText = _uIDocument.rootVisualElement.Q<Label>("HEALTH");
			// _coordinatesText = _uIDocument.rootVisualElement.Q<Label>("COORDINATES");
			// _angleText = _uIDocument.rootVisualElement.Q<Label>("ANGLE");
			// _speedText = _uIDocument.rootVisualElement.Q<Label>("SPEED");
			// _laserCountText = _uIDocument.rootVisualElement.Q<Label>("LASER_COUNT");
			// _laserCooldownText = _uIDocument.rootVisualElement.Q<Label>("LASER_COOLDOWN");

			_viewModel.Health.Subscribe(ChangeHealth).AddTo(_disposable);
			_viewModel.Coordinates.Subscribe(ChangeCoordinates).AddTo(_disposable);
			_viewModel.Angle.Subscribe(ChangeAngle).AddTo(_disposable);
			_viewModel.Speed.Subscribe(ChangeSpeed).AddTo(_disposable);
			_viewModel.LaserCount.Subscribe(ChangeLaserCount).AddTo(_disposable);
			_viewModel.LaserCooldown.Subscribe(ChangeLaserCooldown).AddTo(_disposable);
		}

		public void ChangeHealth(int health) => _healthText.text = $"Health: {health.ToString()}";

		public void ChangeCoordinates(Vector2 coordinate) => _coordinatesText.text = $"Coordinate: {coordinate.ToString()}";

		public void ChangeAngle(float angle) => _angleText.text = $"Angle: {angle.ToString()}";

		public void ChangeSpeed(float speed) => _speedText.text = $"Speed: {speed.ToString()}";

		public void ChangeLaserCount(int count) => _laserCountText.text = $"Laser count: {count.ToString()}";

		public void ChangeLaserCooldown(float time) => _laserCooldownText.text = $"Laser cooldown: {time.ToString()}";
	}

}
