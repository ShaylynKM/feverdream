using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControls : MonoBehaviour
{
    public static VolumeControls Instance;

    [SerializeField]
    private AudioMixer _audioMixer;

    [SerializeField]
    private Slider _volumeSlider;

    // Audio volume in db
    private const float _minValue = -20f;
    private const float _maxValue = 0f;

    private float _currentSavedVolume;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (Instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        _currentSavedVolume = PlayerPrefs.GetFloat("Volume", _maxValue);

        float decibels = Mathf.Lerp(_minValue, _maxValue, _currentSavedVolume); // Convert the saved volume from its linear value

        _audioMixer.SetFloat("MasterVolume", decibels);

        _volumeSlider.value = _currentSavedVolume;
        _volumeSlider.onValueChanged.AddListener(SetVolume); // Adds listener to the slider
    }

    public void SetVolume(float volume)
    {
        _audioMixer.SetFloat("MasterVolume", volume);

        PlayerPrefs.SetFloat("Volume", volume);
    }
}
