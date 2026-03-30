using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    PlayerControls _playerControls;
    PlayerMovement _playerMovement;
    AnimatorManager _animatorManager;
    
    public Vector2 moveDirection;
    public Vector2 cameraInput;

    public float cameraInputX;
    public float cameraInputY;
    
    public float _moveAmount;
    public float verticalInput;
    public float horizontalInput;
    
    public bool sprintInput;
    public bool jumpInput;
    public bool dodgeInput;
    public bool punchInput;

    private void Start()
    {
        Cursor.visible = false;
    
        // Locks the cursor to the center of the game window
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Awake()
    {
        _animatorManager = GetComponent<AnimatorManager>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void OnEnable()
    {
        if (_playerControls == null)
        {
            _playerControls = new PlayerControls();
            
            _playerControls.PlayerMovement.Movement.performed += i => moveDirection = i.ReadValue<Vector2>();
            _playerControls.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
            
            _playerControls.PlayerActions.Shift.performed += i => sprintInput = true;
            _playerControls.PlayerActions.Shift.canceled += i => sprintInput = false;
            
            _playerControls.PlayerActions.Jump.performed += i => jumpInput = true;
            //_playerControls.PlayerActions.Shift.canceled += i => sprintInput = false;
            
            _playerControls.PlayerActions.Dodge.performed += i => dodgeInput = true;
            _playerControls.PlayerActions.Punch.performed += i => punchInput = true;
        }
        _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }

    public void HandleAllInput()
    {
        HandleMovementInput();
        HandleSprintInput();
        HandleJumpInput();
        HandleDodgeInput();
        HandlePunchInput();
    }
    
    private void HandleMovementInput()
    {
        verticalInput = moveDirection.y;
        horizontalInput = moveDirection.x;
        _moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        _animatorManager.UpdateAnimatorValues(0,_moveAmount, _playerMovement.isSprinting);
        
        cameraInputY = cameraInput.y;
        cameraInputX = cameraInput.x;
    }

    private void HandleSprintInput()
    {
        if (sprintInput && _moveAmount > 0.5f)
        {
            _playerMovement.isSprinting = true;
        }
        else
        {
            _playerMovement.isSprinting = false;
        }
    }

    private void HandleJumpInput()
    {
        if (jumpInput)
        {
            jumpInput = false;
            _playerMovement.HandleJumping();
        }
    }

    private void HandleDodgeInput()
    {
        if (dodgeInput)
        {
            dodgeInput = false;
            _playerMovement.HandleDodging();
        }
    }
    
    private void HandlePunchInput()
    {
        if (punchInput)
        {
            punchInput = false;
            _playerMovement.HandlePunching();
            
        }
    }
}
