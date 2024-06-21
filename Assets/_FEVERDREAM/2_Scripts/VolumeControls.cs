using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VolumeControls : MonoBehaviour
{
    public static VolumeControls Instance;

    [SerializeField]
    private AudioMixer _audioMixer;

    [SerializeField]
    private Slider _volumeSlider;

    // Audio volume in db
    private const float _minValue = -80f;
    private const float _maxValue = 0f;

    [SerializeField]
    private float _currentSavedVolume;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        SceneManager.activeSceneChanged += OnActiveSceneChanged; // Subscribes our scene changing method to the callback event

        _currentSavedVolume = PlayerPrefs.GetFloat("Volume", _maxValue); // Assigns the volume as the saved volume (unless there is no saved volume, then the default, maximum level is used

        OnSliderFound();
    }

    void OnActiveSceneChanged(Scene previous, Scene next)
    {
        OnSliderFound();

        SetVolume(_currentSavedVolume);
        Debug.Log("Volume set");
    }

    private void OnDestroy()
    {
        SceneManager.activeSceneChanged -= OnActiveSceneChanged; // Unsubscribes from the callback method when this object is destroyed
    }

    public void SetVolume(float volume)
    {
        _currentSavedVolume = volume;
        PlayerPrefs.SetFloat("Volume", _currentSavedVolume);

        _audioMixer.SetFloat("MasterVolume", volume);

        PlayerPrefs.SetFloat("Volume", volume);
    }

    private void OnSliderFound()
    {
        // Finds and assigns a new volume slider for when the scene changes

        _volumeSlider = FindObjectOfType<Slider>(true);

        if(_volumeSlider != null && _volumeSlider.name == "VolumeSlider")
        {
            _volumeSlider.onValueChanged.AddListener(SetVolume);
            _volumeSlider.value = _currentSavedVolume;
        }

        else
        {
            Debug.Log("Slider not found");
        }

    }

}
