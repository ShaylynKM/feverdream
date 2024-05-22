using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    public DialogueBox dialogueBox = new DialogueBox();

    public static DialogueSystem instance;

    private void Awake()
    {
       if(instance == null)
       {
           instance = this;
       }
       else
       {
           Destroy(gameObject);
       }

    }
}