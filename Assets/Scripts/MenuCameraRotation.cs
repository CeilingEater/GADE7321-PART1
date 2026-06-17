using UnityEngine;

public class MenuCameraRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 5f;
    
    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
