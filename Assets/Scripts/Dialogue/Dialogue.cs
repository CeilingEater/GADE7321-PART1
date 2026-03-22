using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue System/Dialogue Asset")]
public class Dialogue : ScriptableObject
{
    public string name;

    public Sprite dialogueIcon; 
    
    [TextArea(3,10)]
    public string[] sentences;
}

