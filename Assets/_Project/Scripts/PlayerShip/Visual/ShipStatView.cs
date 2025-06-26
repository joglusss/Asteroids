using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace Asteroids.Ship
{
    public class ShipStatView : MonoBehaviour, IInitializable
    {
        [SerializeField] UIDocument _uIDocument;

        private Label _healthText;
        private Label _coordinatesText;
        private Label _angleText;
        private Label _speedText;
        private Label _laserCountText;
        private Label _laserCooldownText;

        public void Initialize()
        {
            _healthText = _uIDocument.rootVisualElement.Q<Label>("HEALTH");
            _coordinatesText = _uIDocument.rootVisualElement.Q<Label>("COORDINATES");
            _angleText = _uIDocument.rootVisualElement.Q<Label>("ANGLE");
            _speedText = _uIDocument.rootVisualElement.Q<Label>("SPEED");
            _laserCountText = _uIDocument.rootVisualElement.Q<Label>("LASER_COUNT");
            _laserCooldownText = _uIDocument.rootVisualElement.Q<Label>("LASER_COOLDOWN");
        }

        public void ChangeHealth(int health) => _healthText.text = "Health: " + health.ToString();

        public void ChangeCoordinates(Vector2 coordinate) => _coordinatesText.text = "Coordinate: " + coordinate.ToString();

        public void ChangeAngle(float angle) => _angleText.text = "Angle: " + angle.ToString();

        public void ChangeSpeed(float speed) => _speedText.text = "Speed: " + speed.ToString();

        public void ChangeLaserCount(int count) => _laserCountText.text = "Laser count: " + count.ToString();

        public void ChangeLaserCooldown(float time) => _laserCooldownText.text = "Laser cooldown: " + time.ToString();

        
    }

}
