using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueAsset : ScriptableObject
{
    [TextArea]
    public string[] DialogueLines; // Lines that show up in the text box

    public string[] OptionText; // Lines that appear on the buttons for player-selected options

    public DialogueAsset[] Options; // Links to scriptable objects based on what option the player chooses
}
