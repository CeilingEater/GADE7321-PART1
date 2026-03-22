using System;
using UnityEditor.Animations;
using UnityEngine;
//using Vector3 = System.Numerics.Vector3;

public class AnimatorManager : MonoBehaviour
{
    public Animator animator;
    PlayerManager playerManager;
    PlayerMovement playerMovement;
    private int _horizontal;
    private int _vertical;
    

    public void Awake()
    {
        animator = GetComponent<Animator>();
        playerManager = GetComponent<PlayerManager>();
        playerMovement = GetComponent<PlayerMovement>();
        _horizontal = Animator.StringToHash("Horizontal");
        _vertical = Animator.StringToHash("Vertical");
    }

    public void PlayTargetAnimation(string targetAnimation, bool isInteracting, bool useRootMotion = false)
    {
        animator.SetBool("isInteracting", isInteracting);
        animator.SetBool("isUsingRootMotion", useRootMotion);
        animator.CrossFade(targetAnimation, 0.2f);
    }
    
    public void UpdateAnimatorValues(float horizontalMovement, float verticalMovement, bool isSprinting)
    {
        float snappedHorizontal;
        float snappedVertical;

        if (horizontalMovement > 0 && horizontalMovement < 0.55f)
        {
            snappedHorizontal = 0.5f;
            
        }
        else if (horizontalMovement > 0.55f)
        {
            snappedHorizontal = 1f;
        }
        else if (horizontalMovement < 0 && horizontalMovement > -0.55f)
        {
            snappedHorizontal = -0.5f;
        }
        else if (horizontalMovement < -0.55f)
        {
            snappedHorizontal = -1f;
        }
        else
        {
            snappedHorizontal = 0f;
        }
        
        //////////////////////////////////////////////
        
        if (verticalMovement > 0 && verticalMovement < 0.55f)
        {
            snappedVertical = 0.5f;
            
        }
        else if (verticalMovement > 0.55f)
        {
            snappedVertical = 1f;
        }
        else if (verticalMovement < 0 && verticalMovement > -0.55f)
        {
            snappedVertical = -0.5f;
        }
        else if (verticalMovement < -0.55f)
        {
            snappedVertical = -1f;
        }
        else
        {
            snappedVertical = 0f;
        }

        if (isSprinting)
        {
            snappedHorizontal = horizontalMovement;
            snappedVertical = 2;
        }
        
        animator.SetFloat(_horizontal, snappedHorizontal,0.1f, Time.deltaTime);
        animator.SetFloat(_vertical, snappedVertical,0.1f, Time.deltaTime);
    }

    private void OnAnimatorMove()
    {
        if (playerManager.isUsingRootMotion)
        {
            playerMovement._playerRb.linearDamping = 0;
            Vector3 deltaPosition = animator.deltaPosition; //idk why it doesnt work w normal vector3
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / Time.deltaTime;
            playerMovement._playerRb.linearVelocity = velocity;
        }
    }
    
}
