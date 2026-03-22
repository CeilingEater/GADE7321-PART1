using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    InputManager _inputManager;
    PlayerManager _playerManager;
    AnimatorManager _animatorManager;
    
    private Vector3 _moveDirection;
    private Transform _cameraObject;
    Rigidbody _playerRb;

    [Header("Falling")] 
    public float inAirTimer;
    public float leapingVelocity;
    public float fallingVelocity;
    public float rayCastHeightOffset = 1f;
    public LayerMask groundLayer;
    
    
    [Header("Movement Flags")]
    public bool isSprinting;
    public bool isGrounded;

    [Header("Movement Speeds")]
    public float walkingSpeed = 1.5f;
    public float runSpeed = 5f;
    public float sprintSpeed = 7f;
    public float rotationSpeed = 15f;

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
        _playerRb.linearVelocity = movementVelocity;
    }

    private void HandleRotation()
    {
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
        RaycastHit hit;
        //Vector3 rayCastOrigin = transform.position;
        Vector3 rayCastOrigin = transform.position + Vector3.up * 1f;
        rayCastOrigin.y = rayCastOrigin.y + rayCastHeightOffset;

        if (!isGrounded)
        {
            if (!_playerManager.isInteracting)
            {
                _animatorManager.PlayTargetAnimation("Falling",true);  
            }
            
            inAirTimer = inAirTimer + Time.deltaTime;
            _playerRb.AddForce(transform.forward * leapingVelocity);
            _playerRb.AddForce(-Vector3.up * fallingVelocity * inAirTimer);
        }

        //if (Physics.SphereCast(rayCastOrigin, 0.2f, Vector3.down, out hit, 1.5f, groundLayer))
        if (Physics.SphereCast(rayCastOrigin, 0.3f, Vector3.down, out hit, 2f))
        {
            if (!isGrounded && _playerManager.isInteracting)
            {
                _animatorManager.PlayTargetAnimation("Landing",true);
            }

            inAirTimer = 0;
            isGrounded = true;
            _playerManager.isInteracting = false; 
        }
        else
        {
            isGrounded = false;
        }
        
        //Debug.Log(isGrounded);
        //Debug.DrawRay(rayCastOrigin, Vector3.down * 1.5f, Color.red);
    }
    
}
