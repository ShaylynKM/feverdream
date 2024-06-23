using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TurnOffPostProcessing : MonoBehaviour
{
    [SerializeField]
    private Toggle _ppToggle; // Nobody say a damn thing about this variable name

    [SerializeField]
    private bool _ppEnabled = true; // Or this one

    private PostProcessVolume _ppVolume; // Or this

    private void Awake()
    {
        _ppEnabled = PlayerPrefs.GetInt("PostProcessingOnOff", 1) == 1; // Applies the post processing effects immediately

        // This script goes on the main camera. Get the volume from it
        _ppVolume = GetComponent<PostProcessVolume>();

        TurnPPOnAndOff();
    }

    private void Start()
    {
        OnToggleFound();
    }

    private void OnToggleFound()
    {
        Toggle[] toggles = FindObjectsOfType<Toggle>(true);

        // Makes sure we find the correct toggle before proceeding
        foreach (Toggle toggle in toggles)
        {
            if (toggle?.name == "PPToggle")
            {
                _ppToggle = toggle;
                break;
            }
        }

        if (_ppToggle != null)
        {
            // Assigns the listener for the value changed
            _ppToggle.onValueChanged.AddListener(delegate
            {
                OnPPToggle(_ppToggle.isOn);
            });

            _ppToggle.isOn = _ppEnabled; // Sets the toggle in the scene to on or off based on the current state
        }
        else
        {
            Debug.Log("Post processing toggle not found");
        }
    }

    private void OnPPToggle(bool isEnabled)
    {
        _ppEnabled = isEnabled;

        PlayerPrefs.SetInt("PostProcessingOnOff", _ppEnabled ? 1 : 0); // Saves the bool in PlayerPrefs as an Int, since PlayerPrefs doesn't natively support boolean values

        TurnPPOnAndOff();

    }

    private void TurnPPOnAndOff()
    {
        // Turns the post process volume on and off based on the bool value

        if (_ppEnabled == true)
        {
            _ppVolume.enabled = true;
        }
        else
        {
            _ppVolume.enabled = false;
        }
    }
}
