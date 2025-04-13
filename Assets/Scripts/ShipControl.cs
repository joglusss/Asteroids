using System;
using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class ShipControl : MonoBehaviour, ISpaceInteract, MenuScript.IResetable
{
    
    [SerializeField] private float forwardSpeed = 5000.0f;
    [SerializeField] private float angularSpeed = -1000.0f;

    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_collider = GetComponent<Collider2D>();

        moveInputAction = InputSystem.actions.FindAction("Move");

        InputSystem.actions.FindAction("Shot").performed += (a) => { if (this.gameObject.activeInHierarchy) Shot(); };
        InputSystem.actions.FindAction("Laser").performed += (a) => {if(this.gameObject.activeInHierarchy) Laser(); };

        StartImmortalFrame();

        if (LaserCooldownCounterCoroutine == null)
        {
            LaserCooldownCounterCoroutine = StartCoroutine(LaserCooldownCounter());
        }

        ((MenuScript.IResetable)this).InitialazeIRessetable();
    }

    private void Update()
    {
        Movement();

        if (ObjectManager.IsOutCanvas(m_rigidbody.position, out Vector2 wallSide))
        {
            ObjectManager.Teleport(m_rigidbody, wallSide);
        }
    }


    private Collider2D m_collider;
    private Rigidbody2D m_rigidbody;
    private InputAction moveInputAction;

    public Vector2 Position { get => m_rigidbody.position;}
    public float Angle { get => m_rigidbody.rotation; }
    public Vector2 Velosity { get => m_rigidbody.linearVelocity; }

    private void Movement() {
        Vector2 inputValue = moveInputAction.ReadValue<Vector2>().normalized;

        Vector2 force = inputValue.y > 0.0f ? (Vector2)transform.up * inputValue.y * forwardSpeed * ObjectManager.speedScale * Time.deltaTime : Vector2.zero;
        float angle = inputValue.x * angularSpeed * Time.deltaTime;

        m_rigidbody.AddForce(force, ForceMode2D.Impulse);
        m_rigidbody.AddTorque(angle);
    }


    [field: Space]
    [field: SerializeField] public float ImmortalTime { get; private set; } = 10.0f;
    private Coroutine ImmortalFrameCounter;
    public bool IsShipImmortal { get { return ImmortalFrameCounter != null; }}
    private void StartImmortalFrame() {
        IEnumerator FrameCounter() {
            m_collider.enabled = false;
            yield return new WaitForSeconds(ImmortalTime);
            m_collider.enabled = true;
            ImmortalFrameCounter = null;
        }

        if (ImmortalFrameCounter == null)
            ImmortalFrameCounter = StartCoroutine(FrameCounter());
    }

    private void Shot() {

        Debug.Log("Shot");
        SpaceObject bullet = ObjectManager.singltone.bullet.DrawObject();
        if (bullet !=null)
        {
            bullet.Launch(this.transform.position + this.transform.up, this.transform.up);
        }
    }

    [field: Space]
    [field: SerializeField] public float LaserCooldownTime { get; private set; } = 4.0f;
    [field: SerializeField] public int MaxLaserCount { get; private set; }
    [field: SerializeField] public int CurrentAvailableLaser { get; private set; }
    public float CurrentLaserCooldown { get; private set; }
    

    private Coroutine LaserCooldownCounterCoroutine;
    private IEnumerator LaserCooldownCounter() {
        while (CurrentAvailableLaser < MaxLaserCount)
        {
            float time = LaserCooldownTime;
            while (time > 0.0f) {
                time = Mathf.Clamp(time - Time.deltaTime, 0.0f, MaxLaserCount);
                CurrentLaserCooldown = time;
                yield return null;
            }

            CurrentAvailableLaser++;
        }

        LaserCooldownCounterCoroutine = null;
    }

    private void Laser() {

        if (CurrentAvailableLaser < MaxLaserCount)
            if (LaserCooldownCounterCoroutine == null)
            {
                LaserCooldownCounterCoroutine = StartCoroutine(LaserCooldownCounter());
            }

        if ( CurrentAvailableLaser > 0) {
            SpaceObject laser = ObjectManager.singltone.laser.DrawObject();
            if (laser != null)
            {
                CurrentAvailableLaser--;
                laser.Launch(this.transform.position + this.transform.up, this.transform.up);
            }
        }
    }
       

    [field: SerializeField] public ISpaceInteract.SpaceObjectTypeEnum SpaceObjectType { get; set; }
    public void Interact(ISpaceInteract.SpaceObjectTypeEnum collisionSpaceObjectType)
    {
        if (collisionSpaceObjectType != ISpaceInteract.SpaceObjectTypeEnum.SpaceShip)
            Hit();
    }

    [field: Space]
    [SerializeField] private int shipHP = 3;
    public int ShipHP { get { return shipHP; } private set { shipHP = value; changingHPEvent?.Invoke(value); }}

    private event Action<int> changingHPEvent;
    public event Action<int> ChangeHPEvent { remove => changingHPEvent -= value; add => changingHPEvent += value;}

    public void Hit()
    {
        ShipHP--;
        StartImmortalFrame();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ISpaceInteract collider))
            Interact(collider.SpaceObjectType);
    }

    public void StopGame(){ }

    public void StartGame()
    {
        shipHP = 3;
        m_rigidbody.position = ObjectManager.canvasSize / 2.0f;
    }
}
