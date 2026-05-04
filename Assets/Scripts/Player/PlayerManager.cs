using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("State Machine")]
    public PlayerBaseState currentState;
    public GroundedState groundedState;
    public InAirState inAirState = new InAirState();
    
    InputManager _inputManager;
    CameraManager _cameraManager;
    PlayerMovement _playerMovement;
    AnimatorManager _animatorManager;
    Animator _animator;
    
    public bool isInteracting;
    public bool isUsingRootMotion;

    private void Start()
    {
        currentState = groundedState;
        currentState.EnterState(this, _playerMovement, _animatorManager);
    }

    private void Awake()
    {
        _inputManager = GetComponent<InputManager>();
        //cameraManager = GetComponent<CameraManager>();
        _cameraManager = FindFirstObjectByType<CameraManager>();
        _playerMovement = GetComponent<PlayerMovement>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _inputManager.HandleAllInput();
        currentState.UpdateState(this);
    }

    private void FixedUpdate()
    {
        _playerMovement.HandleAllMovement();
        currentState.FixedUpdateState(this, _playerMovement);
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
        currentState = newState;
        currentState.EnterState(this, _playerMovement, _animatorManager);
    }
}