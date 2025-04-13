using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Parallax : MonoBehaviour
{
    [SerializeField] float TotalScale;
    [SerializeField] RawImage Background1;
    [SerializeField] float Scale1;
    [SerializeField] RawImage Background2;
    [SerializeField] float Scale2;
    [SerializeField] Rigidbody2D SpaceShipRb;

    private Vector2 position = Vector2.zero;

    public void Update()
    {
        position += SpaceShipRb.linearVelocity * TotalScale;

        Background1.uvRect = new Rect() { x = position.x * Scale1, y = position.y * Scale1 , width = 1, height = 1 };
        Background2.uvRect = new Rect() { x = position.x * Scale2, y = position.y * Scale2, width = 1, height = 1 };
    }
}
