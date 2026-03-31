using System;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public InputManager inputManager;
    public Transform targetTransform;  //the camera will follow this 
    public Transform cameraPivot;  //the camera uses this object to pivot
    public Transform cameraTransform;
    public LayerMask collisionLayers;
    private float defaultPosition;
    private Vector3 _cameraFollowVelocity = Vector3.zero;
    private Vector3 _cameraVectorPosition = Vector3.zero;

    public float cameraCollisionOffset = 0.2f;
    public float minCollisionOffset = 0.2f;
    public float cameraCollisionRadius = 0.2f;
    public float cameraFollowSpeed = 0.2f;

    public float lookAngle;  //up and down
    public float pivotAngle;  //left and right
    
    public float cameraLookSpeed = 2f;
    public float cameraPivotSpeed = 2f;
    public float minPivot = -35f;
    public float maxPivot = 35f;
    
    private void Awake()
    {
        inputManager = FindFirstObjectByType<InputManager>();
        targetTransform = FindFirstObjectByType<PlayerManager>().transform;
        cameraTransform = Camera.main.transform;
        defaultPosition = cameraTransform.localPosition.z;
    }

    public void HandleAllCameraMovement()
    {
        FollowTarget();
        RotateCamera();
        HandleCameraCollisions();
    }

    private void FollowTarget()
    {
        Vector3 targetPosition = Vector3.SmoothDamp(
            transform.position,  targetTransform.position, ref _cameraFollowVelocity, cameraFollowSpeed);
        transform.position = targetPosition;
    }

    private void RotateCamera()
    {
        Vector3 rotation;
        lookAngle = lookAngle + (inputManager.cameraInputX * cameraLookSpeed);
        pivotAngle = pivotAngle + (inputManager.cameraInputY * cameraPivotSpeed);
        pivotAngle = Mathf.Clamp(pivotAngle, minPivot, maxPivot);
        
        rotation = Vector3.zero;
        rotation.y = lookAngle;
        Quaternion targetRotation = Quaternion.Euler(rotation);
        transform.rotation = targetRotation;
        
        rotation = Vector3.zero;
        rotation.x = pivotAngle;
        targetRotation = Quaternion.Euler(rotation);
        cameraPivot.localRotation = targetRotation;
    }

    private void HandleCameraCollisions()
    {
        float targetPosition = defaultPosition;
        RaycastHit hit;
    
        Vector3 direction = cameraTransform.position - cameraPivot.position;
        direction.Normalize();

        if (Physics.SphereCast(cameraPivot.position, cameraCollisionRadius, direction, out hit, Mathf.Abs(defaultPosition), collisionLayers))
        {
            float distance = Vector3.Distance(cameraPivot.position, hit.point);
            targetPosition = -(distance - cameraCollisionOffset);
        }

        if (Mathf.Abs(targetPosition) < minCollisionOffset)
        {
            targetPosition = targetPosition - minCollisionOffset;
        }

        _cameraVectorPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPosition, Time.deltaTime / 0.2f);
        cameraTransform.localPosition = _cameraVectorPosition;
    }
}
