using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D),typeof(TouchingHandler))]

public class PlayerController : Controller
{
    private readonly float defaultJumpSpeed = 6f, defaultWalkSpeed = 5f, defaultRunSpeed = 12f;

    private TimeManager tM;
    private GhostManager ghM;
    private GameManager gM;
    public PlayerInputActions pInput;
    private InputAction move, jump, interact, warp, drop, read;
    private Animator animator;
    private TouchingHandler touchH;
    public Rigidbody2D rb;

    public bool canTimeTravel;
    public Vector2 moveInput;
    private bool holdingRun = false;
    private float jumpSpeed = 6f, walkSpeed = 5f, runSpeed = 12f;
    private bool _isMoving = false;
    private bool _isRunning = false;
    [SerializeField]
    private bool _isJumping = false;
    [SerializeField]
    private bool _isBufferingJump = false;
    [SerializeField]
    private bool _isHoldingDrop = false;
    [SerializeField]
    private bool _isInteracting = false;
    private IEnumerator jumpBuffer, jumpHold;


    

    public float currentMoveSpeed {
        get {
            if(CanMove) {
                if (IsMoving && !touchH.IsOnWall) {
                    if(IsRunning) {
                        return runSpeed;
                    } else {
                        return walkSpeed;
                    }
                } else {
                    return 0;
                }
            } else {
                return 0;
            } 
        }
    }
    
    public bool IsMoving { 
        get { 
            return _isMoving;
        } private set {
            _isMoving = value;
            animator.SetBool(AnimStr.isMoving,value);
        }
    }

    public bool IsRunning
    {
        get {
            return _isRunning;
        } private set {
            _isRunning = value;
            animator.SetBool(AnimStr.isRunning, value);
        }
    }
    
    public bool IsInteracting
    {
        get { 
            return _isInteracting;
        } private set {
            _isInteracting = value;
            animator.SetBool(AnimStr.isInteracting,value);
        }
    }

    // If the coroutine is running and this value changes, end the coroutine. If the value is true, start a coroutine
    public bool IsBufferingJump {
        get {
            return _isBufferingJump;
        } set {
            if(_isBufferingJump) {
                StopCoroutine(jumpBuffer);
            }
            // if(!_isBufferingJump && value) {
            if(value) {
                jumpBuffer = BufferJump();
                StartCoroutine(jumpBuffer);
            }
            _isBufferingJump = value;
        }
    }

    public bool IsJumping {
        get {
            return _isJumping;
        } set {
            // if(!_isJumping && value) {
            if(value && !_isJumping) {
                jumpHold = HoldJump();
                StartCoroutine(jumpHold);
                animator.SetTrigger(AnimStr.jump);
            } else if(!value) {
                StopCoroutine(jumpHold);
            }
            _isJumping = value;
        }
    }

    public bool CanMove { 
        get {
            return animator.GetBool(AnimStr.canMove);
        }
    }

    public bool IsHoldingDrop
    {
        get { return _isHoldingDrop; }
        set
        {
            _isHoldingDrop = value;
            if (value)
            {
                Physics.IgnoreLayerCollision(8, 6, true);
            }
            else
            {
                Physics.IgnoreLayerCollision(8, 6, false);
            }
        }
    }

    public void ScaleMovementSpeed(float scale)
    {
        jumpSpeed = defaultJumpSpeed * scale;
        walkSpeed = defaultWalkSpeed * scale;
        runSpeed = defaultRunSpeed * scale;
    }

    public override void Die() {
        
    }
    public void OnMove(InputAction.CallbackContext context) 
    {
        moveInput = context.ReadValue<Vector2>();
        IsMoving = moveInput != Vector2.zero;
        SetFacingDirection(moveInput.x);
        if(context.started && holdingRun) {
            IsRunning = true;
        }
        else if(context.canceled) {
            IsRunning = false;
        }
    }

    

    public void OnDrop(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsHoldingDrop = true;
        }
        else if (context.canceled)
        {
            IsHoldingDrop = false;
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsRunning = true;
            holdingRun = true;
        }
        else if (context.canceled)
        {
            IsRunning = false;
            holdingRun = false;
        }
    }

    public void OnInteractAction(InputAction.CallbackContext context)
    {
        IsInteracting = context.started;
        OnInteract(context.started);
    }
    
    public void OnWarp(InputAction.CallbackContext context)
    {
        if(gM.canTimeTravel)
        {
            int newTime = gM.GetStartFrame();
            Vector3 newPos = gM.GetStartPos();
            tM.SetTime(newTime - 1);
            gM.TimeTravel();
            transform.position = newPos;
        } else {Debug.Log("Can't warp");}
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.started) {
            IsBufferingJump = true;
        } else if(context.canceled) {
            IsBufferingJump = false;
            if(IsJumping) {
                IsJumping = false;
            }
        }
    }
    public void StartJump()
    {
        if (touchH.IsGrounded && CanMove)
        {
            IsJumping = true;
        }
    }
    private IEnumerator BufferJump()
    {
        yield return new WaitForSeconds(0.2f);
        IsBufferingJump = false;
    }
    
    private IEnumerator HoldJump() 
    {
        yield return new WaitForSeconds(0.3f);
        IsJumping = false;
    }
    public void OnRead(InputAction.CallbackContext context)
    {
        GameObject.Find("Manager").GetComponent<SaveManager>().PrintData();
    }

    void FixedUpdate() {
        float velocityX = moveInput.x * currentMoveSpeed;
        float velocityY = rb.linearVelocity.y;
        if(IsBufferingJump) {
            StartJump();
        }
        if(IsJumping) {
            velocityY = jumpSpeed;
        }
        rb.linearVelocity = new Vector2(velocityX, velocityY);
        animator.SetFloat(AnimStr.yVelocity, velocityY);
    }

    private void OnEnable() {
        Debug.Log("Enabling");
        pInput.Enable();
        pInput.Player.Enable();
        move.Enable();
        move.started += OnMove; move.performed += OnMove; move.canceled += OnMove;
        jump.started += OnJump; jump.canceled += OnJump;
        warp.started += OnWarp;
        interact.started += OnInteractAction; interact.canceled += OnInteractAction;
        drop.started += OnDrop; drop.canceled += OnDrop;
        read.started += OnRead;

    }

    private void OnDisable() {
        pInput.Disable();
        pInput.Player.Disable();
        move.Disable();
        move.started -= OnMove; move.performed -= OnMove; move.canceled -= OnMove;
        jump.started -= OnJump; jump.canceled -= OnJump;
        warp.started -= OnWarp;
        interact.started -= OnInteractAction; interact.canceled -= OnInteractAction;
        drop.started -= OnDrop; drop.canceled -= OnDrop;
        read.started -= OnRead;
    }

    private void GetManagers()
    {
        tM = GameObject.Find("Manager").GetComponent<TimeManager>();
        ghM = GameObject.Find("Manager").GetComponent<GhostManager>();
        gM = GameObject.Find("Manager").GetComponent<GameManager>();
    }

    private void GetComponents()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchH = GetComponent<TouchingHandler>();
    }

    private void GetActions()
    {
        pInput = new PlayerInputActions();
        Debug.Log("PlayerController:" + pInput);
        UIManager UIMan = GameObject.Find("Manager").GetComponent<UIManager>();
        UIMan.AssignInput(pInput);
        UIMan.enabled = true;
        move = pInput.Player.Move;
        jump = pInput.Player.Jump;
        warp = pInput.Player.Warp;
        interact = pInput.Player.Interact;
        drop = pInput.Player.Drop;
        read = pInput.Player.Crouch;
        jumpBuffer = BufferJump();
        jumpHold = HoldJump();
    }

    private void Awake()
    {
        id = 0;
        currentTouching = new SortedList<int, GameObject>();
        GetManagers();
        GetActions();
        GetComponents();
    }
}