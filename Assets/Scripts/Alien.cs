
using UnityEngine;
using UnityEngine.InputSystem;

public class Alien : Bullet
{
    [SerializeField] public float AngularSpeed;

    [SerializeField] private Transform target;
    void Update()
    {
        Vector2 force = (Vector2)transform.up * Speed * ObjectManager.speedScale * Time.deltaTime;
        float angle =   Vector2.SignedAngle(transform.up, target.position - transform.position) * AngularSpeed * Time.deltaTime;

        m_rigidbody.AddForce(force, ForceMode2D.Impulse);
        m_rigidbody.AddTorque(angle);
    }
}
