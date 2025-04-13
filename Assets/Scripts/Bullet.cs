using System.Collections;
using UnityEngine;

public class Bullet : SpaceObject, ISpaceInteract
{
    [field: SerializeField] public float Speed { get; set; }


    protected Rigidbody2D m_rigidbody;
    public override void OnStartGame()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    public override void Launch(Vector2 from, Vector2 direction)
    {
        m_rigidbody.transform. position = from;
        m_rigidbody.AddForce(direction.normalized * ObjectManager.speedScale * Speed, ForceMode2D.Impulse);

        coroutineLifetimeCounter = StartCoroutine(LifetimeCounter());
    }

    

    [field: SerializeField] public ISpaceInteract.SpaceObjectTypeEnum SpaceObjectType { get; set; }
    public void Interact(ISpaceInteract.SpaceObjectTypeEnum collisionSpaceObjectType)
    {
        if (SpaceObjectType != collisionSpaceObjectType)
            Demolish(); 
    }

    public virtual void Demolish() {
        
        if(coroutineLifetimeCounter != null)
            StopCoroutine(coroutineLifetimeCounter);
        ReturnDelegate.Invoke();
    }
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ISpaceInteract spaceObject))
            Interact(spaceObject.SpaceObjectType);
    }
}
