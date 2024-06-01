using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [SerializeField]
    private DialogueSO _dialogueInformation; // SO containing the lines of dialogue

    [SerializeField]
    private Button _dialogueBoxButton; // The dialogue box itself is a button that can be clicked to advance dialogue

    [SerializeField]
    private TextMeshProUGUI _speakerNameText;

    [SerializeField]
    private TextMeshProUGUI _dialogueText;

    [SerializeField]
    public Action OnDialogueComplete; // Event triggered when dialogue is complete

    private bool _completeCurrentSentence = false;

    private Queue<DialogueLine> _lines; // Queue that holds all dialogue lines for the current dialogue session

    private bool _isTyping = false; // Flag if text is currently being typed

    private float _typingSpeed = 0.05f; // Speed text characters are revealed in dialogue box


    private void Awake()
    {
        _lines = new Queue<DialogueLine>(); // Initializes the queue
    }

    private void Start()
    {
        StartDialogue(_dialogueInformation.lines);
        Debug.Log("started dialogue");
    }

    public void StartDialogue(List<DialogueLine> dialogueLines)
    {
        _lines.Clear(); // Empties the queue

        foreach(DialogueLine dialogueLine in dialogueLines)
        {
            _lines.Enqueue(dialogueLine);
            Debug.Log("cued lines");
        }

        DisplayNextDialogueLine();
    }

    public void DisplayNextDialogueLine()
    {
        Debug.Log("Displaying dialogue");
        // If we are currently typing, complete the current sentence immediately
        if(_isTyping == true)
        {
            _completeCurrentSentence = true; // Ensures the typing coroutine is stopped in TypeSentence
            return;
        }

        // If there are no more lines to display, end the dialogue.
        if(_lines.Count == 0)
        {
            Debug.Log("no more lines");
            EndDialogue();
            return;
        }

        // Dequeues the next line and updates UI elements accordingly
        DialogueLine currentLine = _lines.Dequeue();
        _speakerNameText.text = currentLine.Speaker.SpeakerName;

        StopAllCoroutines();

        // Starts typing the next dialogue line
        StartCoroutine(TypeSentence(currentLine));
        
    }

    IEnumerator TypeSentence(DialogueLine dialogueLine)
    {
        _dialogueText.text = ""; // Clears the current text
        _isTyping = true; // It's typing! Wow.
        _completeCurrentSentence = false;

        // Types out each character in the dialogue line at the specified typing speed
        foreach(char letter in dialogueLine.Line.ToCharArray())
        {
            if (_completeCurrentSentence)
            {
                _dialogueText.text = dialogueLine.Line;
                break;
            }
            else
            {
                _dialogueText.text += letter;
                yield return new WaitForSecondsRealtime(_typingSpeed);
            }
        }

        _isTyping = false;// Indicates that typing is complete.
        _completeCurrentSentence = false;

        // I'd like to refractor this to change how many characters are visible rather than typing them out one by one. This will allow me to use format tags.
    }

    // Should be attached to the dialogue box button
    public void OnDialogueBoxClick()
    {
        DisplayNextDialogueLine();
    }

    public void EndDialogue()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        int nextSceneIndex = currentSceneIndex + 1;

        if(nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log("load next scene");
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("The game is over. Go home."); // Will fill this in when I have something to put at the end of the game.
        }
    }
}

[System.Serializable]
public class DialogueLine
{
    public SpeakerNameLabel Speaker;
    public string Line;
}

[System.Serializable]
public class SpeakerNameLabel
{
    public string SpeakerName;
}

[System.Serializable]
public class Dialogue
{
    public List<DialogueLine> DialogueLines = new List<DialogueLine>();
}
