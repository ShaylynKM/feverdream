using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(TMP_Text))]
public class ClickLinkedTextHandler : MonoBehaviour, IPointerClickHandler
{
    private TextMeshProUGUI _textBox;

    private Canvas _canvasToCheck;

    [SerializeField]
    private Camera _cameraToUse;

    public void OnPointerClick(PointerEventData eventData)
    {
        Vector3 mousePosition = new Vector3(eventData.position.x, eventData.position.y, 0);
        var linkTaggedText = TMP_TextUtilities.FindIntersectingLink(_textBox, mousePosition, _cameraToUse);

        if (linkTaggedText != -1) //Means there is a link under our cursor
        {
            TMP_LinkInfo linkInfo = _textBox.textInfo.linkInfo[linkTaggedText];
            Debug.Log("clicky");
        }
    }

    private void Awake()
    {
        _textBox = GetComponent<TextMeshProUGUI>();
        _canvasToCheck = GetComponentInParent<Canvas>();

        if(_canvasToCheck.renderMode == RenderMode.ScreenSpaceOverlay)
        {
            _cameraToUse = null;
        }
        else
        {
            _cameraToUse = _canvasToCheck.worldCamera;
        }
    }
}
