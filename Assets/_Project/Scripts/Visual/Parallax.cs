using UnityEngine;

namespace Asteroids.Visual {
    public class Parallax : MonoBehaviour
    {
        [SerializeField] float _totalScale;
        [SerializeField] SpriteRenderer _background;
        [SerializeField] float _scale1;
        [SerializeField] float _scale2;
        [SerializeField] Rigidbody2D _spaceShipRb;

        private Vector2 position = Vector2.zero;

        private void Update()
        {
            position += _spaceShipRb.linearVelocity * _totalScale;
            _background.material.SetVector("_Offset_1", new Vector2(position.x * _scale2, position.y * _scale2));
            _background.material.SetVector("_Offset_2", new Vector2(position.x * _scale1, position.y * _scale1));
        }
    }

}
