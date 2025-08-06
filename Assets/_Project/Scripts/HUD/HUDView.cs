using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace Asteroids.Visual
{
	public class HUDView
	{ 
		[SerializeField] UIDocument _uIDocument;

		private Label _healthText;
		private Label _coordinatesText;
		private Label _angleText;
		private Label _speedText;
		private Label _laserCountText;
		private Label _laserCooldownText;

		[Inject]
		private void Construct()
		{ 
			
		}

		public void Initialize()
		{
			_healthText = _uIDocument.rootVisualElement.Q<Label>("HEALTH");
			_coordinatesText = _uIDocument.rootVisualElement.Q<Label>("COORDINATES");
			_angleText = _uIDocument.rootVisualElement.Q<Label>("ANGLE");
			_speedText = _uIDocument.rootVisualElement.Q<Label>("SPEED");
			_laserCountText = _uIDocument.rootVisualElement.Q<Label>("LASER_COUNT");
			_laserCooldownText = _uIDocument.rootVisualElement.Q<Label>("LASER_COOLDOWN");
			
			
		}

		private void ChangeHealth(int health) => _healthText.text = "Health: " + health.ToString();

		private void ChangeCoordinates(Vector2 coordinate) => _coordinatesText.text = "Coordinate: " + coordinate.ToString();

		private void ChangeAngle(float angle) => _angleText.text = "Angle: " + angle.ToString();

		private void ChangeSpeed(float speed) => _speedText.text = "Speed: " + speed.ToString();

		private void ChangeLaserCount(int count) => _laserCountText.text = "Laser count: " + count.ToString();

		private void ChangeLaserCooldown(float time) => _laserCooldownText.text = "Laser cooldown: " + time.ToString();
	}
}
