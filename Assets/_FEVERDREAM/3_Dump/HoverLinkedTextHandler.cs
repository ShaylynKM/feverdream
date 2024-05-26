using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[RequireComponent(typeof(TMP_Text))]
public class HoverLinkedTextHandler : MonoBehaviour
{
    private TextMeshProUGUI _textBox;

    private Canvas _canvasToCheck;

    [SerializeField]
    private Camera _cameraToUse;

    private RectTransform _textBoxRectTransform;

    private int _currentlyActiveLinkedElement;

    public UnityEvent MouseHovering;


    private void Awake()
    {
        _textBox = GetComponent<TextMeshProUGUI>();
        _canvasToCheck = GetComponentInParent<Canvas>();
        _textBoxRectTransform = GetComponent<RectTransform>();

        if(_canvasToCheck.renderMode == RenderMode.ScreenSpaceOverlay)
        {
            _cameraToUse = null;
        }
        else
        {
            _cameraToUse = _canvasToCheck.worldCamera;
        }
    }

    private void Update()
    {
        CheckForLinkAtMousePosition();
    }

    private void CheckForLinkAtMousePosition()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue(); // Specifically uses the new input system

        bool isIntersectingRectTransform = TMP_TextUtilities.IsIntersectingRectTransform(_textBoxRectTransform, mousePosition, _cameraToUse); // Currently hovering over text box

        if (!isIntersectingRectTransform)
            return;

        int intersectingLink = TMP_TextUtilities.FindIntersectingLink(_textBox, mousePosition, _cameraToUse);

        if (intersectingLink == -1)
            return; // No link found

        TMP_LinkInfo linkInfo = _textBox.textInfo.linkInfo[intersectingLink]; // If a link is found, get its info

        Debug.Log("hovering");

        MouseHovering.Invoke();
    }
}
