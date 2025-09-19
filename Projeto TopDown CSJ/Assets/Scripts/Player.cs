using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public bool isPaused;
    private bool isAttacking;
    [SerializeField] private float speed;
    [SerializeField] private float runSpeed;

    private Rigidbody2D rig;

    private float initialSpeed;
    private bool isRunning;
    private bool isRolling;
    private bool isCutting;
    private bool isDigging;
    private bool isWatering;
    private Vector2 _direction;
    private int handlingObj;
    private PlayerInventory playerInventory;

    public Vector2 direction
    {
        get { return _direction; }
        set { _direction = value; }
    }
    
    public bool IsRunning { get => isRunning; set => isRunning = value; }
    public bool IsRolling { get => isRolling; set => isRolling = value; }
    public bool IsCutting { get => isCutting; set => isCutting = value; }
    public bool IsDigging { get => isDigging; set => isDigging = value;}
    public bool IsWatering { get => isWatering; set => isWatering = value; }
    public bool IsAttacking { get => isAttacking; set => isAttacking = value; }
    public int HandlingObj { get => handlingObj; set => handlingObj = value; }
    public float Speed { get => speed; set => speed = value; }

    void Start()
    {
        initialSpeed = Speed;
        rig = GetComponent<Rigidbody2D>();
        playerInventory = GetComponent<PlayerInventory>();
    }

    void Update()
    {
        if (!isPaused)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) { HandlingObj = 0; }
            if (Input.GetKeyDown(KeyCode.Alpha2)) { HandlingObj = 1; }
            if (Input.GetKeyDown(KeyCode.Alpha3)) { HandlingObj = 2; }
            if (Input.GetKeyDown(KeyCode.Alpha4)) { HandlingObj = 3; }

            OnInput();
            OnRun();
            OnRoll();

            switch (HandlingObj)
            {
                case 0: OnAttacking(); break;
                case 1: OnCutting(); break;
                case 2: OnDigging(); break;
                case 3: OnWatering(); break;
            }
            OnActionRelease();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("teste");
        }
    }

    void FixedUpdate()
    {
        if (!isPaused)
        {
            OnMove();
        }
    }

    #region Movement

    void OnCutting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isCutting = true;
            Speed = 0;
        }
    }

    void OnDigging()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDigging = true;
            Speed = 0;
        }
    }

    void OnWatering()
    {
        if (Input.GetMouseButtonDown(0) && playerInventory.CurrentWater > 0)
        {
            isWatering = true;
            Speed = 0;
        }

        if (isWatering)
        {
            playerInventory.CurrentWater -= 0.1f;
        }

        if (playerInventory.CurrentWater <= 0)
        {
            isWatering = false;
        }
    }

    void OnAttacking()
    {
        if (Input.GetMouseButtonDown(0))
        {
            IsAttacking = true;
            Speed = 0;
        }
    }

    void OnActionRelease()
    {
        if (Input.GetMouseButtonUp(0))
        {
            isCutting = false;
            isDigging = false;
            isWatering = false;
            IsAttacking = false;
            Speed = initialSpeed;
        }
    }

    void OnInput()
    {
        _direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    void OnMove()
    {
        rig.MovePosition(rig.position + _direction * Speed * Time.fixedDeltaTime);
    }

    void OnRun()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Speed = runSpeed;
            isRunning = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            if (isDigging || isCutting) { Speed = 0; }
            else { Speed = initialSpeed; }
            isRunning = false;
        }
    }

    void OnRoll()
    {
        isRolling = Input.GetMouseButton(1);
    }
    
    #endregion
}
