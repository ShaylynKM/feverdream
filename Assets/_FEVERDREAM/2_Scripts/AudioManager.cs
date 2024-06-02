using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEditor.Build;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Singleton
    private static AudioManager _instance;

    public static AudioManager Instance { get { return _instance;} }
    #endregion


    public float AudioVolume = 0.5f;

    [SerializeField]
    private AudioSO _audioFiles;

    private Dictionary<AudioNames, AudioSource> _audioSources = new Dictionary<AudioNames, AudioSource>();

    private void Awake()
    {

        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        foreach (AudioPairs entry in _audioFiles.MyAudioPairs)
        {
            AudioSource source = Instantiate(entry.AudioValue); // Instantiates audio prefab
            source.loop = entry.IsLooped; // Whether the audio should be looped or not
            _audioSources.Add(entry.AudioKey, source);
        }
    }

    private void Start()
    {
        UpdateVolume(AudioVolume);
    }

    public void UpdateVolume(float volume)
    {
        this.AudioVolume = volume;

        foreach(KeyValuePair<AudioNames, AudioSource> entry in _audioSources)
        {
            entry.Value.volume = volume; // Updates the volume of each track
        }
    }

    public void Play(AudioNames name)
    {
        if(_audioSources.ContainsKey(name))
        {
            
            _audioSources[name].Play();
        }
    }

    AudioClip GetAudioClip(AudioNames name)
    {
        return _audioSources[name].clip;
    }

    public void StopAudio(AudioNames name)
    {
        _audioSources[name]?.Stop();
    }
}
