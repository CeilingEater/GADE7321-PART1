using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    InputManager _inputManager;
    CameraManager _cameraManager;
    PlayerMovement _playerMovement;
    Animator _animator;
    
    public bool isInteracting;
    public bool isUsingRootMotion;
    
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
    }

    private void FixedUpdate()
    {
        _playerMovement.HandleAllMovement();
    }

    private void LateUpdate()
    {
        _cameraManager.HandleAllCameraMovement();
        
        isInteracting = _animator.GetBool("isInteracting");
        isUsingRootMotion = _animator.GetBool("isUsingRootMotion");
        
        _playerMovement.isJumping = _animator.GetBool("isJumping");
        
        _animator.SetBool("isGrounded", _playerMovement.isGrounded);
    }
}
