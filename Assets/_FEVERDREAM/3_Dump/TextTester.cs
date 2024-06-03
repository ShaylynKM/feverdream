using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TESTING
{
    // This is just something to quickly test the text scripts. Not a finalized script by any means. Don't come at me for using the old input system (yet)
    public class TextTester : MonoBehaviour
    {
        DialogueSystem dialogueSystem;
        TextArchitect architect;

        string[] lines = new string[5]
        {
            "Well, here we go everybody",
            "I'm testing out my scripts",
            "Sure hope it works",
            "Otherwise I might backflip out my bedroom window",
            "Pray for me, y'all"
        };

        private void Start()
        {
            dialogueSystem = DialogueSystem.instance;
            architect = new TextArchitect(dialogueSystem.dialogueBox.DialogueText);
        }

        private void Update()
        {
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    architect.Build(lines[Random.Range(0, lines.Length)]);
                }

                if (Input.GetKeyDown(KeyCode.Z))
                {
                    architect.CompleteTextOnClick();
                }

                // IT WORKS LET'S FUCKING GO
            }
            
        }
    }
}

