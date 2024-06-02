using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEditor.VersionControl;
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

    [SerializeField]
    private string _nextSceneName;

    [SerializeField]
    private bool _hasChoice; // Option to have choices the player can select

    [SerializeField]
    private Canvas _choiceCanvas;

    private bool _completeCurrentSentence = false;

    private Queue<DialogueLine> _lines; // Queue that holds all dialogue lines for the current dialogue session

    private bool _isTyping = false; // Flag if text is currently being typed

    [SerializeField]
    private float _typingSpeed = 0.05f; // Speed text characters are revealed in dialogue box

    [SerializeField]
    private float _dialogueLoad = 0.1f;

    private void Awake()
    {
        _lines = new Queue<DialogueLine>(); // Initializes the queue
        _choiceCanvas.enabled = false;
    }

    private void Start()
    {
        StartCoroutine(WaitForLoad());
    }

    private IEnumerator WaitForLoad()
    {
        _dialogueText.text = ""; // clears text
        _dialogueBoxButton.interactable = false; // keeps player from pressing button until dialogue loads

        yield return new WaitForSeconds(_dialogueLoad); // wait time for readability or dramatic effect

        _dialogueBoxButton.interactable = true; // resets the button
        StartDialogue(_dialogueInformation.lines);
        Debug.Log("started dialogue");
    }

    public void StartDialogue(List<DialogueLine> dialogueLines)
    {
        _lines.Clear(); // Empties the queue

        foreach (DialogueLine dialogueLine in dialogueLines)
        {
            _lines.Enqueue(dialogueLine);
            Debug.Log("cued lines");
        }

        DisplayNextDialogueLine();
    }

    public void DisplayNextDialogueLine()
    {
        // If we are currently typing, complete the current sentence immediately
        if (_isTyping == true)
        {
            _completeCurrentSentence = true; // Ensures the typing coroutine is stopped in TypeSentence
            _isTyping = false;
            return;
        }

        // If there are no more lines to display, end the dialogue.
        if (_lines.Count == 0)
        {
            if (_hasChoice == true)
            {
                _dialogueBoxButton.interactable = false; // Keeps the player from clicking the button over and over
                _choiceCanvas.enabled = true;
            }
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
        _dialogueText.text = dialogueLine.Line;

        string fullText = _dialogueText.text;

        _dialogueText.maxVisibleCharacters = 0;

        _dialogueText.text = fullText;

        int dialogueLineCharLength = fullText.Length;

        _isTyping = true;

        _completeCurrentSentence = false;

        // Displays each character in the dialogue line at the specified typing speed
        while (_dialogueText.maxVisibleCharacters < dialogueLineCharLength)
        {
            if (_completeCurrentSentence)
            {
                _dialogueText.maxVisibleCharacters = dialogueLineCharLength;

                break;
            }

            _dialogueText.maxVisibleCharacters++; // Increase the amount of visible characters one by one
            yield return new WaitForSecondsRealtime(_typingSpeed);
        }

        _isTyping = false;// Indicates that typing is complete.
        _completeCurrentSentence = false;
    }

    // Should be attached to the dialogue box button
    public void OnDialogueBoxClick()
    {
        DisplayNextDialogueLine();
    }

    public void EndDialogue()
    {
        if (_nextSceneName != "") // If the string for the next scene is not left empty
        {
            Debug.Log("load next scene");
            SceneManager.LoadScene(sceneName: _nextSceneName); // Loading by name in case I have to go back to certain scenes, such as in the case of a choice.
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
