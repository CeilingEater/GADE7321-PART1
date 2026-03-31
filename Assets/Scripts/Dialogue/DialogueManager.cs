using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Image iconHolder;
    
    public Animator animator;
    
    private QueueNode<string> sentences; 
    
    //private Queue<string> sentences;

    void Start()
    {
        sentences = new QueueNode<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);
        
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        
        nameText.text = dialogue.name;
        if (dialogue.dialogueIcon != null) iconHolder.sprite = dialogue.dialogueIcon;
        
        sentences = new QueueNode<string>();
        
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
        
    }

    public void DisplayNextSentence()
    {
        if (sentences.IsEmpty)
        {
            EndDialogue();
            return;
        }
        
        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
        Debug.Log(sentence);
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }

    void EndDialogue()
    {
        Debug.Log("EndDialogue called");
        dialogueText.text = "";
        animator.SetBool("IsOpen", false);
        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

}
