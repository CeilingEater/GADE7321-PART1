using UnityEngine;

public class MovingPlatformZ : MonoBehaviour
{
    public float speed = 3.0f;
    public float distance = 5.0f; 
    
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float newZ = Mathf.PingPong(Time.time * speed, distance); //lol pingpong
        transform.position = new Vector3(startPos.x, startPos.y, startPos.z + (newZ - (distance / 2))); 
        //this just keeps the x and y the same and makes it loop
    }
}