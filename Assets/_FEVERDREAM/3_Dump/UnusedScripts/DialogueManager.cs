using Ink.Parsed;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private DialogueAsset _dialogueAsset;

    [SerializeField]
    private TextMeshProUGUI _text;
    
    public static DialogueManager instance;

    private bool _completeOnClick = false; // Click twice to complete the current line.

    private bool _typing = false; // Currently building the text
    private float _typeSpeed = 5f;

    int _currentLineIndex = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("clicked");

    }

    private void StartDialogue()
    {
        _typing = true;
    }
}

