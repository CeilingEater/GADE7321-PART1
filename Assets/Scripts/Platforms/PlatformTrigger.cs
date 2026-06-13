using UnityEngine;

public class PlatformTrigger : MonoBehaviour //used to be on actual platforms but i made 'colliders' seperate to avoid the collision of the actual tile. i didnt bother removing the color stuff
{
    public GameObject[] platformsToActivate; 
    public bool deactivateOnExit = false; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetPlatformsActive(true);
            GetComponent<Renderer>().material.color = Color.green;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (deactivateOnExit && other.CompareTag("Player"))
        {
            SetPlatformsActive(false);
            GetComponent<Renderer>().material.color = Color.red;
        }
    }

    void SetPlatformsActive(bool state)
    {
        foreach (GameObject platform in platformsToActivate)
        {
            if (platform != null) platform.SetActive(state);
        }
    }
}