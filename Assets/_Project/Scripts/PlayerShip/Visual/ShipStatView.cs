using TMPro;
using UnityEngine;

namespace Asteroids.Ship
{
    public class ShipStatView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _healthText;
        [SerializeField] private TMP_Text _coordinatesText;
        [SerializeField] private TMP_Text _angleText;
        [SerializeField] private TMP_Text _speedText;
        [SerializeField] private TMP_Text _laserCountText;
        [SerializeField] private TMP_Text _laserCooldownText;

        public void ChangeHealth(int health) => _healthText.text = "Health: " + health.ToString();

        public void ChangeCoordinates(Vector2 coordinate) => _coordinatesText.text = "Coordinate: " + coordinate.ToString();

        public void ChangeAngle(float angle) => _angleText.text = "Angle: " + angle.ToString();

        public void ChangeSpeed(float speed) => _speedText.text = "Speed: " + speed.ToString();

        public void ChangeLaserCount(int count) => _laserCountText.text = "Laser count: " + count.ToString();

        public void ChangeLaserCooldown(float time) => _laserCooldownText.text = "Laser cooldown: " + time.ToString();
    }

}
