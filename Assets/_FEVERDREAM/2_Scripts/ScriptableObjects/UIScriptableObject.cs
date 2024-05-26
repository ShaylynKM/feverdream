using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class UIScriptableObject : ScriptableObject
{
    public Sprite Background;
    public Sprite DialogueBox;

    public Button UpArrow;
    public Button DownArrow;
    public Button LeftArrow;
    public Button RightArrow;

    public TextMeshProUGUI DialogueText;

    public Button MainMenuButton;
    public Button SettingsButton;
    public Button SaveLoadButton;
    public Button QuitButton;

    public Button[] OptionButtons;

    // Will need dialogue option buttons. Currently deciding how I plan to do this. Want to potentially do what you can do in Twine where certain words are clickable.
}
