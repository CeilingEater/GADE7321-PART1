using UnityEngine;
    public class DialogueZone : MonoBehaviour
    {
        public Dialogue dialogue;
        private bool _hasBeenTriggered = false;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && !_hasBeenTriggered)
            {
                _hasBeenTriggered = true;
            
               
                DialogueManager manager = FindFirstObjectByType<DialogueManager>();
                if (manager != null)
                {
                    manager.StartDialogue(dialogue);
                }
                
            }
        }
    }

