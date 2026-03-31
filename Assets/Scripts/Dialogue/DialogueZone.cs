using UnityEngine;
    public class DialogueZone : MonoBehaviour
    {
        public Dialogue dialogue; // Drag your ScriptableObject here
        private bool _hasBeenTriggered = false;

        private void OnTriggerEnter(Collider other)
        {
            // Check if the Player entered the zone and we haven't talked yet
            if (other.CompareTag("Player") && !_hasBeenTriggered)
            {
                _hasBeenTriggered = true;
            
                // Find the manager and start the talk
                DialogueManager manager = FindFirstObjectByType<DialogueManager>();
                if (manager != null)
                {
                    manager.StartDialogue(dialogue);
                }
            
                // Optional: Destroy this trigger if you only want it to happen ONCE ever
                // Destroy(gameObject); 
            }
        }
    }

