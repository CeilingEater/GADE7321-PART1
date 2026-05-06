using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("State Machine")]
    public PlayerBaseState currentState;
    public WalkingState walkingState = new WalkingState();
    public RunningState runningState = new RunningState();
    public IdleState idleState = new IdleState();
    public JumpingState jumpingState = new JumpingState();
    
    [HideInInspector] public InputManager _inputManager;
    [HideInInspector] public CameraManager _cameraManager;
    [HideInInspector] public PlayerMovement _playerMovement;
    [HideInInspector] public AnimatorManager _animatorManager;
    [HideInInspector] public Animator _animator;
    
    public bool isInteracting;
    public bool isUsingRootMotion;

    private void Start()
    {
        SwitchState(idleState);
        //currentState = idleState;
        //currentState.EnterState(this, _playerMovement, _animatorManager);
    }

    private void Awake()
    {
        _inputManager = GetComponent<InputManager>();
        //cameraManager = GetComponent<CameraManager>();
        _cameraManager = FindFirstObjectByType<CameraManager>();
        _playerMovement = GetComponent<PlayerMovement>();
        _animatorManager = GetComponent<AnimatorManager>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _inputManager.HandleAllInput();

        if (currentState != null)
        {
            currentState.UpdateState(this);
        }
    }

    private void FixedUpdate()
    {
        //_playerMovement.HandleAllMovement();
        if (currentState != null)
        {
            currentState.FixedUpdateState(this, _playerMovement);
        }
    }

    private void LateUpdate()
    {
        _cameraManager.HandleAllCameraMovement();
        
        isInteracting = _animator.GetBool("isInteracting");
        isUsingRootMotion = _animator.GetBool("isUsingRootMotion");
        
        _playerMovement.isJumping = _animator.GetBool("isJumping");
        
        _animator.SetBool("isGrounded", _playerMovement.isGrounded);
    }

    public void SwitchState(PlayerBaseState newState)
    {
        /*if (currentState != null)
        {
            currentState.ExitState(this);
        }*/
        
        currentState = newState;
        currentState.EnterState(this, _playerMovement, _animatorManager);
    }
}