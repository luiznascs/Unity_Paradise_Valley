using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isPaused;

    [SerializeField] private float speed;
    [SerializeField] private float runSpeed;


    private Rigidbody2D rig;
    private PlayerItems playerItems;

    private float initialSpeed;
    private bool _isRunning;
    private bool _isRolling;
    private bool _isCutting;
    private bool _isDigging;
    private bool _isWatering;


    private Vector2 _direction; // _ por questoes de nomenclatura
    
    [HideInInspector] public int handlingObj;

    public Vector2 direction{  // permite que a variável direction seja acessada em outro script sem torná-la public
        get {return _direction;}
        set {_direction = value;}
    }
    
    public bool isRunning
    {  // permite que a variável direction seja acessada em outro script sem torná-la public
        get {return _isRunning;}
        set {_isRunning = value;}
    }

    public bool isRolling
    {  // permite que a variável direction seja acessada em outro script sem torná-la public
        get {return _isRolling;}
        set {_isRolling = value;}
    }

    public bool isCutting
    {
        get{return _isCutting;} 
        set{_isCutting = value;}
    }

    public bool isDigging
    {
        get{return _isDigging;} 
        set{_isDigging = value;}
    }

    public bool isWatering
    {  // permite que a variável direction seja acessada em outro script sem torná-la public
        get {return _isWatering;}
        set {_isWatering = value;}
    }

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        playerItems = GetComponent<PlayerItems>();
        initialSpeed = speed;    
    }

    private void Update()
    {
        if(!isPaused)
        {
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                handlingObj = 0;
            }

            if(Input.GetKeyDown(KeyCode.Alpha2))
            {
                handlingObj = 1;
            }

            if(Input.GetKeyDown(KeyCode.Alpha3))
            {
                handlingObj = 2;
            }
        
            OnInput();

            OnRun();

            OnRolling();

            OnCutting();

            OnDig();

            OnWatering();
        }
        
    }

    private void FixedUpdate() // utiliza coisas relacionadas a física
    {
        if(!isPaused)
        {
            OnMove();
        }
    }

    // organiza um bloco de código
    #region Movement

    void OnWatering()
    {
        if(handlingObj == 2)
        {
            if(Input.GetMouseButtonDown(0) && playerItems.currentWater > 0)
            {
                isWatering = true;
                speed = 0f;
            }

            if(Input.GetMouseButtonUp(0) || playerItems.currentWater < 0)
            {
                isWatering = false;
                speed = initialSpeed;
            }

            if(isWatering)
            {
                playerItems.currentWater -= 0.01f;
            }
        }

    }


    void OnDig()
    {
        if(handlingObj == 1)
        {
            if(Input.GetMouseButtonDown(0))
            {
                isDigging = true;
                speed = 0f;
            }

            if(Input.GetMouseButtonUp(0))
            {
                isDigging = false;
                speed = initialSpeed;
            }
        }

    }


    void OnCutting()
    {
        if(handlingObj == 0)
        {
            if(Input.GetMouseButtonDown(0))
            {
                isCutting = true;
                speed = 0f;
            }

            if(Input.GetMouseButtonUp(0))
            {
                isCutting = false;
                speed = initialSpeed;
            }
        }
    }
    void OnInput()
    {
        _direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    void OnMove()
    {
        rig.MovePosition(rig.position + _direction * speed * Time.fixedDeltaTime); // permite mover o corpo em uma direção
    }
    
    void OnRun()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = runSpeed;
            _isRunning = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = initialSpeed;
            _isRunning = false;
        }
    }

    void OnRolling()
    {
        if(Input.GetMouseButtonDown(1))
        {
            _isRolling = true;
        }

        if(Input.GetMouseButtonUp(1))
        {
            speed = initialSpeed;
            _isRolling = false;
        }
    }
    #endregion
}
