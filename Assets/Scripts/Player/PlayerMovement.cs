using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    InputManager _inputManager;
    PlayerManager _playerManager;
    AnimatorManager _animatorManager;
    
    private Vector3 _moveDirection;
    private Transform _cameraObject;
    public Rigidbody _playerRb;

    [Header("Falling")] 
    public float inAirTimer;
    public float leapingVelocity;
    public float fallingVelocity;
    public float rayCastHeightOffset = 1f;
   // public LayerMask groundLayer;
    
    [Header("Ground Check")]
    public float groundCheckDistance = 0.2f;
    public LayerMask groundLayer;
    
    [Header("Movement Flags")]
    public bool isSprinting;
    public bool isGrounded;
    public bool isJumping;

    [Header("Movement Speeds")]
    public float walkingSpeed = 1.5f;
    public float runSpeed = 5f;
    public float sprintSpeed = 7f;
    public float rotationSpeed = 15f;

    [Header("Jump Speeds")] 
    public float jumpHeight = 3f;
    public float gravityIntensity = -15f;

    public void Awake()
    {
        _inputManager = GetComponent<InputManager>();
        _playerManager = GetComponent<PlayerManager>();
        _animatorManager = GetComponent<AnimatorManager>();
        _playerRb = GetComponent<Rigidbody>();
        _cameraObject = Camera.main.transform;
    }

    public void HandleAllMovement()
    {
        HandleFallingAndLanding();
        if (_playerManager.isInteracting)
            return;
        HandleMovement();
        HandleRotation();
        
    }
    
    private void HandleMovement()
    {
        if (isJumping)
        {
            return;
        }
        
        _moveDirection = _cameraObject.forward * _inputManager.verticalInput;
        _moveDirection = _moveDirection + _cameraObject.right * _inputManager.horizontalInput;
        _moveDirection.Normalize();
        _moveDirection.y = 0;
        
        //sprint vs run section
        if (isSprinting)
        {
            _moveDirection *= sprintSpeed;
        }
        else
        {
            if (_inputManager._moveAmount >= 0.5f)
            {
                _moveDirection *= runSpeed;
            }
            else
            {
                _moveDirection *= walkingSpeed;
            }
        }
        
        _moveDirection = _moveDirection * runSpeed;

        Vector3 movementVelocity = _moveDirection;
        movementVelocity.y = _playerRb.linearVelocity.y;
        _playerRb.linearVelocity = movementVelocity;
    }
    
    

    private void HandleRotation()
    {
        if (isJumping)
        {
            return;
        }
        
        Vector3 targetDirection = Vector3.zero;

        targetDirection = _cameraObject.forward * _inputManager.verticalInput;
        targetDirection = targetDirection + _cameraObject.right * _inputManager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;
        targetDirection = targetDirection * rotationSpeed;

        if (targetDirection == Vector3.zero)
        {
            targetDirection = transform.forward;
        }
        
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        
        transform.rotation = playerRotation;
    }
    

    private void HandleFallingAndLanding()
    {
        // Start the ray slightly above the feet
        Vector3 rayCastOrigin = transform.position + (Vector3.up * 0.1f);
    
        // Check for ground. 
        // IMPORTANT: Make sure the 'groundLayer' in Inspector is set to 'Default' 
        // AND the Player's own Layer is NOT 'Default' (set Player to a 'Ignore Raycast' or 'Player' layer).
        isGrounded = Physics.Raycast(rayCastOrigin, Vector3.down, groundCheckDistance + 0.1f, groundLayer);

        if (!isGrounded)
        {
            inAirTimer += Time.deltaTime;
        
            // This manually adds extra gravity if Unity's built-in gravity isn't enough
            _playerRb.AddForce(Vector3.down * fallingVelocity * inAirTimer, ForceMode.Acceleration);
        }
        else
        {
            inAirTimer = 0;
            // If we are grounded, make sure we aren't still trying to 'fall'
            if (_playerRb.linearVelocity.y < 0) 
            {
                // This stops the 'vibrating' on the floor
                Vector3 vel = _playerRb.linearVelocity;
                vel.y = 0;
                _playerRb.linearVelocity = vel;
            }
        }
    }

    public void HandleJumping()
    {
        if (isGrounded)
        {
            _animatorManager.animator.SetBool("isJumping", true);
            _animatorManager.PlayTargetAnimation("Jump",false);
            
            float jumpingVelocity = Mathf.Sqrt(-2 * gravityIntensity * jumpHeight);
            Vector3 playerVelocity = _moveDirection;
            playerVelocity.y = jumpingVelocity;
            _playerRb.linearVelocity = playerVelocity;
        }
    }

    public void HandleDodging()
    {
        if (_playerManager.isInteracting)
            return;
        _animatorManager.PlayTargetAnimation("Dodge",true, true);
        //invulnerability
    }
    
    public void HandlePunching()
    {
        if (_playerManager.isInteracting)
            return;
        _animatorManager.PlayTargetAnimation("Punch",true, true);
    }
    
}
