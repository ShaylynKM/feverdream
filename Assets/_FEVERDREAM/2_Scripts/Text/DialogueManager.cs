using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
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
    private GameObject _choiceObject;

    private bool _completeCurrentSentence = false;

    private Queue<DialogueLine> _lines; // Queue that holds all dialogue lines for the current dialogue session

    private bool _isTyping = false; // Flag if text is currently being typed

    [SerializeField]
    private float _typingSpeed = 0.05f; // Speed text characters are revealed in dialogue box

    [SerializeField]
    private float _dialogueLoad = 0.1f;

    private AudioSource _audioSource;

    private bool _insideFormatTag = false; // For making sure the text sounds don't play for format tags

    private void Awake()
    {
        _lines = new Queue<DialogueLine>(); // Initializes the queue
        _choiceObject.SetActive(false);

        _audioSource = GetComponentInChildren<AudioSource>();

        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

    }

    private void Start()
    {
        StartCoroutine(WaitForLoad());
        MusicManager.Instance.PlayMusic(_dialogueInformation.audioData);

    }

    private IEnumerator WaitForLoad()
    {
        _dialogueText.text = ""; // clears text
        _speakerNameText.text = ""; // clears speaker name
        _dialogueBoxButton.interactable = false; // keeps player from pressing button until dialogue loads

        yield return new WaitForSeconds(_dialogueLoad); // wait time for readability or dramatic effect

        _dialogueBoxButton.interactable = true; // resets the button
        StartDialogue(_dialogueInformation.lines);
    }

    public void StartDialogue(List<DialogueLine> dialogueLines)
    {
       
        _lines.Clear(); // Empties the queue

        foreach (DialogueLine dialogueLine in dialogueLines)
        {
            _lines.Enqueue(dialogueLine);
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
                _choiceObject.SetActive(true);
            }
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

        int currentIndex = 0; // The index of the character we are currently typing


        // Displays each character in the dialogue line at the specified typing speed
        while (_dialogueText.maxVisibleCharacters < dialogueLineCharLength)
        {
            if (PauseMenuManager.Instance.IsPaused)
            {
                yield return new WaitUntil(() => !PauseMenuManager.Instance.IsPaused); // Avoid typing while the game is paused
            }

            if (_completeCurrentSentence)
            {
                _dialogueText.maxVisibleCharacters = dialogueLineCharLength;

                break;
            }

            char currentTypedCharacter = fullText[currentIndex]; // Which character is about to be revealed

            // Checks to see if we are currently inside a format tag (used to keep the text sound from playing for characters that are not visually revealed
            if(currentTypedCharacter == '<')
            {
                _insideFormatTag = true;
            }
            else if(currentTypedCharacter == '>')
            {
                _insideFormatTag = false;
            }

            if(_insideFormatTag == false)
            {
                _dialogueText.maxVisibleCharacters++; // Increase the amount of visible characters one by one (only if they are not part of a format tag)

                if (_audioSource.isPlaying == false && dialogueLine.SpeakerVoice != null)
                {
                    _audioSource.clip = dialogueLine.SpeakerVoice; // Use the audio from the scriptable object
                    _audioSource.Play();
                }
                yield return new WaitForSecondsRealtime(_typingSpeed);
            }
            else
            {
                yield return null;
            }

            currentIndex++; // Continues to increment characters, even if they aren't being revealed (in the case of a format tag)
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
            SceneManager.LoadScene(sceneName: _nextSceneName); // Loading by name in case I have to go back to certain scenes, such as in the case of a choice.
        }
    }
}

[System.Serializable]
public class DialogueLine
{
    public SpeakerNameLabel Speaker;
    public string Line;
    public AudioClip SpeakerVoice;
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
