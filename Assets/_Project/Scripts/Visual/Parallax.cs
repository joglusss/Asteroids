using UnityEngine;
using Zenject;
using Asteroids.Ship;

namespace Asteroids.Visual {
	public class Parallax : MonoBehaviour
	{
		[SerializeField] private float _totalScale;
		[SerializeField] private SpriteRenderer _background;
		[SerializeField] private float _scale1;
		[SerializeField] private float _scale2;

		private Rigidbody2D _spaceShipRb;
		private Vector2 position = Vector2.zero;

		[Inject]
        private void Construct(ShipControl shipControl)
        {
            _spaceShipRb = shipControl.gameObject.GetComponent<Rigidbody2D>();
        }

		private void Update()
		{
			position += _spaceShipRb.linearVelocity * _totalScale;
			_background.material.SetVector("_Offset_1", new Vector2(position.x * _scale2, position.y * _scale2));
			_background.material.SetVector("_Offset_2", new Vector2(position.x * _scale1, position.y * _scale1));
		}


	}

}
